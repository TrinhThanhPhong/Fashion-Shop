import torch
import pickle
from model import IntentClassifier
from preprocessing import load_data_and_build_vocab, preprocess_text, tokenize
from sklearn.model_selection import train_test_split, KFold
from sklearn.metrics import precision_score, recall_score, f1_score, accuracy_score, classification_report
import numpy as np

# Định nghĩa hàm chuẩn bị dữ liệu để dự đoán
def prepare_data_for_prediction(data, vocab, device):
    X = []
    y = []
    max_len = max(len(seq) for seq, _ in data)
    for seq, label in data:
        padded_seq = seq + [vocab.word2idx['<unk>']] * (max_len - len(seq))
        X.append(padded_seq)
        y.append(label)
    X = torch.tensor(X, dtype=torch.long).to(device)
    y = torch.tensor(y, dtype=torch.long).to(device)
    return X, y, max_len

# Định nghĩa hàm dự đoán trên batch
def predict_batch(model, X, device):
    model.eval()
    predictions = []
    with torch.no_grad():
        outputs = model(X)
        _, predicted = torch.max(outputs, 1)
    return predicted

# Hàm chia dữ liệu thành train/test (dùng cho đánh giá train/test thông thường)
def split_data(data, vocab, intent_labels, test_size=0.2, random_state=42):
    all_patterns = []
    all_labels = []
    for intent in data['intents']:
        label_id = intent_labels[intent['tag']]
        for pattern in intent['patterns']:
            pattern_processed = preprocess_text(pattern)
            sentence_indices = [
                vocab.word2idx.get(word, vocab.word2idx['<unk>'])
                for word in tokenize(pattern_processed)
            ]
            all_patterns.append(sentence_indices)
            all_labels.append(label_id)

    train_patterns, test_patterns, train_labels, test_labels = train_test_split(
        all_patterns, all_labels, test_size=test_size, random_state=random_state, stratify=all_labels
    )

    train_data = [(pattern, label) for pattern, label in zip(train_patterns, train_labels)]
    test_data = [(pattern, label) for pattern, label in zip(test_patterns, test_labels)]

    return train_data, test_data

# Hàm chuẩn bị dữ liệu cho cross-validation
def prepare_data_for_cross_validation(data, vocab, intent_labels):
    all_patterns = []
    all_labels = []
    for intent in data['intents']:
        label_id = intent_labels[intent['tag']]
        for pattern in intent['patterns']:
            pattern_processed = preprocess_text(pattern)
            sentence_indices = [
                vocab.word2idx.get(word, vocab.word2idx['<unk>'])
                for word in tokenize(pattern_processed)
            ]
            all_patterns.append(sentence_indices)
            all_labels.append(label_id)

    all_data = [(pattern, label) for pattern, label in zip(all_patterns, all_labels)]
    return all_data

# Hàm thực hiện k-fold cross-validation
def k_fold_cross_validation(data, vocab, intent_labels, label_to_intent, model, device, n_splits=5):
    all_data = prepare_data_for_cross_validation(data, vocab, intent_labels)
    kf = KFold(n_splits=n_splits, shuffle=True, random_state=42)

    accuracy_scores = []
    precision_scores = []
    recall_scores = []
    f1_scores = []

    fold = 1
    for train_idx, test_idx in kf.split(all_data):
        print(f"\nFold {fold}/{n_splits}:")
        train_data = [all_data[i] for i in train_idx]
        test_data = [all_data[i] for i in test_idx]

        X_test, y_test, _ = prepare_data_for_prediction(test_data, vocab, device)
        predicted = predict_batch(model, X_test, device)

        y_pred = predicted.cpu().numpy()
        y_true = y_test.cpu().numpy()

        accuracy = accuracy_score(y_true, y_pred)
        precision = precision_score(y_true, y_pred, average='weighted', zero_division=0)
        recall = recall_score(y_true, y_pred, average='weighted', zero_division=0)
        f1 = f1_score(y_true, y_pred, average='weighted', zero_division=0)

        accuracy_scores.append(accuracy)
        precision_scores.append(precision)
        recall_scores.append(recall)
        f1_scores.append(f1)

        print(f"Accuracy: {accuracy:.4f}")
        print(f"Precision (weighted): {precision:.4f}")
        print(f"Recall (weighted): {recall:.4f}")
        print(f"F1-Score (weighted): {f1:.4f}")

        # Lấy danh sách các nhãn hợp lệ từ y_true và ánh xạ chính xác
        unique_labels = np.unique(y_true)
        valid_labels = [label for label in unique_labels if label in label_to_intent]
        target_names = [label_to_intent[label] for label in valid_labels if label in label_to_intent]

        print("\nBáo cáo chi tiết cho fold hiện tại:")
        print(classification_report(y_true, y_pred, labels=valid_labels, target_names=target_names, zero_division=0))
        
        fold += 1

    avg_accuracy = np.mean(accuracy_scores)
    avg_precision = np.mean(precision_scores)
    avg_recall = np.mean(recall_scores)
    avg_f1 = np.mean(f1_scores)

    std_accuracy = np.std(accuracy_scores)
    std_precision = np.std(precision_scores)
    std_recall = np.std(recall_scores)
    std_f1 = np.std(f1_scores)

    print("\nTổng kết Cross-Validation (trung bình qua các fold):")
    print(f"Accuracy: {avg_accuracy:.4f} ± {std_accuracy:.4f}")
    print(f"Precision (weighted): {avg_precision:.4f} ± {std_precision:.4f}")
    print(f"Recall (weighted): {avg_recall:.4f} ± {std_recall:.4f}")
    print(f"F1-Score (weighted): {avg_f1:.4f} ± {std_f1:.4f}")

    return avg_accuracy, avg_precision, avg_recall, avg_f1

# Hàm chính để đánh giá mô hình
def evaluate_model():
    # Load dữ liệu
    json_path = 'data.json'
    data, vocab, intent_labels, label_to_intent, train_data = load_data_and_build_vocab(json_path)

    # Thiết lập các tham số mô hình
    input_size = len(vocab)
    hidden_size = 128
    embedding_dim = 64
    num_classes = len(intent_labels)
    device = torch.device('cuda' if torch.cuda.is_available() else 'cpu')

    # Load mô hình đã huấn luyện
    model = IntentClassifier(input_size, hidden_size, embedding_dim, num_classes).to(device)
    model.load_state_dict(torch.load("Nino.pth"))
    model.eval()

    # Đánh giá bằng train/test split
    print("Đánh giá bằng train/test split:")
    train_data, test_data = split_data(data, vocab, intent_labels, test_size=0.2)
    
    X_test, y_test, max_len = prepare_data_for_prediction(test_data, vocab, device)
    predicted = predict_batch(model, X_test, device)

    y_pred = predicted.cpu().numpy()
    y_true = y_test.cpu().numpy()

    accuracy = accuracy_score(y_true, y_pred)
    precision = precision_score(y_true, y_pred, average='weighted', zero_division=0)
    recall = recall_score(y_true, y_pred, average='weighted', zero_division=0)
    f1 = f1_score(y_true, y_pred, average='weighted', zero_division=0)

    print("Hiệu suất mô hình trên tập test (train/test split):")
    print(f"Accuracy: {accuracy:.4f}")
    print(f"Precision (weighted): {precision:.4f}")
    print(f"Recall (weighted): {recall:.4f}")
    print(f"F1-Score (weighted): {f1:.4f}")

    print("\nBáo cáo chi tiết cho tập test (train/test split):")
    print(classification_report(y_true, y_pred, target_names=[label_to_intent[i] for i in range(len(intent_labels))], zero_division=0))

    X_train, y_train, _ = prepare_data_for_prediction(train_data, vocab, device)
    train_pred = predict_batch(model, X_train, device)
    train_accuracy = accuracy_score(y_train.cpu().numpy(), train_pred.cpu().numpy())
    print(f"\nAccuracy trên tập train: {train_accuracy:.4f}")
    print(f"Chênh lệch Accuracy (train - test): {(train_accuracy - accuracy):.4f}")
    if train_accuracy - accuracy > 0.1:
        print("Cảnh báo: Mô hình có dấu hiệu overfitting!")

    # Đánh giá bằng cross-validation
    print("\n" + "="*50 + "\nĐánh giá bằng 5-fold Cross-Validation:\n" + "="*50)
    avg_accuracy, avg_precision, avg_recall, avg_f1 = k_fold_cross_validation(
        data, vocab, intent_labels, label_to_intent, model, device, n_splits=5
    )

if __name__ == "__main__":
    evaluate_model()