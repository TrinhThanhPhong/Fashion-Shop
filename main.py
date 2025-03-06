import torch
from model import IntentClassifier
from preprocessing import load_data_and_build_vocab, preprocess_text, tokenize
from train import predict_intent, get_response
from getProducts import get_products
json_path = 'data.json'
data, vocab, intent_labels, label_to_intent, train_data = load_data_and_build_vocab(json_path)
input_size = len(vocab)  # Đảm bảo rằng vocab đã được định nghĩa đúng
hidden_size = 128
embedding_dim = 64
num_classes = len(intent_labels)  # Đảm bảo intent_labels đã được định nghĩa

device = torch.device('cuda' if torch.cuda.is_available() else 'cpu')

model = IntentClassifier(
    input_size=input_size,
    hidden_size=hidden_size,
    embedding_dim=embedding_dim,
    num_classes=num_classes
).to(device)

# Tải lại trọng số đã lưu
model.load_state_dict(torch.load("Nino.pth"))
model.eval()

#Định nghĩa trường hợp chat
def chat(msg):
    intent_tag = predict_intent(msg)
    if intent_tag == "Hỏi sản phẩm": #trường hợp 1 số lượng sản phẩm
        return get_response(intent_tag) + get_products()
    #Trường hợp thông tin 1 sản phẩm được hỏi
    response = get_response(intent_tag)
    return response