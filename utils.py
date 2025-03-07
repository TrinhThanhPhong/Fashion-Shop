import re

def extract_product_code(question):
    pattern = r'\b[A-Za-z]{1}\d{3}\b'  # Regex cho mã sản phẩm (1 chữ cái, 3 số)
    matches = re.findall(pattern, question)  # Tìm tất cả các mã code phù hợp
    return matches  # Trả về danh sách các mã code

# if __name__ == "__main__":
#     msg = "còn size của A009 không shop ơi?"
#     print("Mã code trích xuất được: ", extract_product_code(msg))

