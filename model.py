import torch
import torch.nn as nn

class IntentClassifier(nn.Module):
    def __init__(self, input_size, hidden_size, embedding_dim, num_classes, dropout=0.4):
        super(IntentClassifier, self).__init__()
        self.embedding = nn.Embedding(input_size, embedding_dim)
        self.dropout1 = nn.Dropout(dropout)
        self.rnn = nn.LSTM(embedding_dim, hidden_size, batch_first=True, bidirectional=True)
        self.dropout2 = nn.Dropout(dropout)
        self.fc = nn.Linear(hidden_size * 2, num_classes)  # *2 vì có 2 hướng

    def forward(self, x):
        # x: [batch_size, seq_len]
        embedded = self.embedding(x)
        embedded = self.dropout1(embedded)
        _, (hidden, _) = self.rnn(embedded)  # hidden: [2, batch_size, hidden_size] - 2 là do bidirectional
        # Kết hợp hidden state từ cả hai hướng
        hidden = torch.cat((hidden[-2,:,:], hidden[-1,:,:]), dim=1)
        hidden = self.dropout2(hidden)
        output = self.fc(hidden)      # [batch_size, num_classes]
        return output
