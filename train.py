import torch
import torch.nn as nn
import torch.optim as optim
import random
from preprocessing import load_data_and_build_vocab, preprocess_text, tokenize
from model import IntentClassifier
import torch.nn.functional as F
#Đọc data và xây dựng vocab
json_path = 'data.json'
data, vocab, intent_labels, label_to_intent, train_data = load_data_and_build_vocab(json_path)

#Khởi tạo mô hình, loss, optimizer
input_size = len(vocab)
hidden_size = 128
embedding_dim = 64
num_classes = len(intent_labels)

device = torch.device('cuda' if torch.cuda.is_available() else 'cpu')

model = IntentClassifier(
    input_size=input_size,
    hidden_size=hidden_size,
    embedding_dim=embedding_dim,
    num_classes=num_classes
).to(device)

criterion = nn.CrossEntropyLoss()
optimizer = optim.Adam(model.parameters(), lr=0.001)

#Huấn luyện mô hình
epochs = 50
for epoch in range(epochs):
    # total_loss = 0

    for sentence_indices, label in train_data:
        # sentence_indices là list chỉ mục
        sentence_tensor = torch.tensor(sentence_indices, dtype=torch.long).unsqueeze(0).to(device)
        label_tensor = torch.tensor([label], dtype=torch.long).to(device)

        # Forward
        output = model(sentence_tensor)
        loss = criterion(output, label_tensor)

        # Backward
        optimizer.zero_grad()
        loss.backward()
        optimizer.step()

        # total_loss += loss.item()

    # avg_loss = total_loss / len(train_data)
    # print(f'Epoch [{epoch+1}/{epochs}], Loss: {avg_loss:.4f}')

#Hàm dự đoán intent
def predict_intent(sentence, threshold=0.4):
    if not sentence:
        return None

    model.eval()
    sentence_processed = preprocess_text(sentence)
    indices = [vocab.word2idx.get(w, vocab.word2idx['<unk>']) for w in tokenize(sentence_processed)]
    sentence_tensor = torch.tensor(indices, dtype=torch.long).unsqueeze(0).to(device)

    with torch.no_grad():
        output = model(sentence_tensor)  # output shape: [1, num_classes]
        probs = F.softmax(output, dim=1)  # Chuyển logits thành xác suất
        confidence, predicted_label = torch.max(probs, dim=1)
        print(confidence.item())
        if confidence.item() < threshold:
            return None  # Không đủ tự tin để trả lời

    intent_tag = label_to_intent[predicted_label.item()]
    return intent_tag

#Hàm lấy phản hồi ngẫu nhiên
def get_response(intent_tag):
    for intent in data['intents']:
        if intent['tag'] == intent_tag:
            return random.choice(intent['responses'])
    return "Xin lỗi, tôi không hiểu."
# #chat
# print("Chatbot đã sẵn sàng! Gõ 'exit' để thoát.")
# while True:
#     user_input = input("Bạn: ")
#     if user_input.lower() == 'exit':
#         break
    
#     # Dự đoán intent
#     intent_tag = predict_intent(user_input)
#     # Lấy phản hồi
#     response = get_response(intent_tag)
#     print("Chatbot:", response)


# torch.save(model.state_dict(), "Nino.pth")
import pickle

# Sau khi huấn luyện mô hình
torch.save(model.state_dict(), "Nino.pth")
# Lưu vocab
with open("vocab.pkl", "wb") as f:
    pickle.dump(vocab, f)
