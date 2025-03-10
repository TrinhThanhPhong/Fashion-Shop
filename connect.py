import pyodbc
import pandas as pd
def create_connection():
    conn_str = (
        "DRIVER={ODBC Driver 17 for SQL Server};"
        # Quang chạy thì nhớ đổi tên SERVER tương ứng máy em nhé
        "SERVER=DESKTOP-B1JGQ9E;"  
        "DATABASE=FashionShop;"
        "Trusted_Connection=yes;"             
    )
    
    try:
        conn = pyodbc.connect(conn_str)
        # print("Kết nối SQL Server thành công!")
        return conn
    except Exception as e:
        print("Lỗi kết nối:", e)
        return None

def check(conn):
    query = "SELECT Title, ProductBrand, ProductType, Quantity FROM dbo.Tb_Product"
    try:
        cursor = conn.cursor()
        cursor.execute(query)
        rows = cursor.fetchall()
        if rows:
            for row in rows:
                print(row)
        else:
            print("Bảng dbo.Tb_Product không có dữ liệu.")
        print("Kiểu: ", type(rows))
    except Exception as e:
        print("Lỗi khi truy vấn bảng dbo.Tb_Product:", e)
        

# def get_total_by_brand(conn):
#     query = """
#     SELECT ProductBrand,ProductType, SUM(Quantity) AS TotalQuantity
#     FROM dbo.Tb_Product
#     GROUP BY ProductBrand
#     """
#     try:
#         cursor = conn.cursor()
#         cursor.execute(query)
#         rows = cursor.fetchall()
        
#         if rows:
#             print(" Tổng số lượng sản phẩm theo thương hiệu")
#             # In tiêu đề cột tạm thời
#             print(f"{'ProductBrand':<20}  {'TotalQuantity':>10}")
#             print("-" * 32)
#             for row in rows:
#                 brand = row[0]
#                 total_qty = row[1]
#                 print(f"{brand:<20} {total_qty:>10} ")
#         else:
#             print("Không có dữ liệu khi tính tổng.")
#     except Exception as e:
#         print("Lỗi khi truy vấn tổng số lượng theo thương hiệu:", e)
def get_in4Product_byCode(code):
    '''
    Lấy thông tin sản phẩm theo mã sản phẩm (code), bao gồm tên, giá và số lượng.
    '''
    query = """
    SELECT Title, Quantity, Price 
    FROM dbo.Tb_Product
    WHERE ProductCode = ? 
    """
    try:
        conn = create_connection()
        if conn:
            cursor = conn.cursor()
            cursor.execute(query, (code,))  # Truyền tham số (code) vào câu truy vấn
            row = cursor.fetchone()  # Lấy 1 dòng dữ liệu đầu tiên

            if row:
                product_in4 = {
                    'Tên sản phẩm: ': row[0],
                    'Tổng số lượng size: ': row[1],
                    'Giá: ': row[2]
                }
                return product_in4
            else:
                print(f"Không tìm thấy sản phẩm với mã {code}.")
                return None
        else:
            print("Không thể kết nối đến với cơ sở dữ liệu!")
            return None
    except Exception as e:
        print(f"Lỗi khi truy vấn sản phẩm theo mã {code}: {e}")
        return None


def get_total_by_brand_and_type(conn):
    """
    Hàm hiển thị ra tổng số Quantity cho mỗi (ProductType, ProductBrand).
    """

    query = """
    SELECT 
        ProductType,
        ProductBrand,
        SUM(Quantity) AS TotalQuantity
    FROM dbo.Tb_Product
    GROUP BY ProductType, ProductBrand

    """
    try:
        cursor = conn.cursor()
        cursor.execute(query)
        rows = cursor.fetchall()
        lst_prod = {
                        'Kiểu quần áo': [],
                        'Hãng sản phẩm': [],
                        'Số lượng còn lại': []
                    }
        if rows:
            for row in rows:
                # Chú ý: Tùy thứ tự cột SELECT, row[0] = ProductType, row[1] = ProductBrand, row[2] = TotalQuantity
                '''
                list _information = {
                                        'product_type': [row[0]],
                                        'product_brand': [row[1]],
                                        'total_qty': [row[2]]
                                    }
                '''
                product_type = row[0]
                lst_prod["Kiểu quần áo"].append(product_type)
                product_brand = row[1]
                lst_prod["Hãng sản phẩm"].append(product_brand)
                total_qty = row[2]
                lst_prod["Số lượng còn lại"].append(total_qty)
                # print(f"{product_type} - {product_brand}: {total_qty}")
            # print(lst_prod)
            return(lst_prod)
        else:
            print("Không có dữ liệu khi tính tổng.")
    except Exception as e:
        print("Lỗi khi truy vấn tổng số lượng theo ProductType và ProductBrand:", e)


def get_listProducts():
    conn = create_connection()
    if conn:
        # check(conn)
        df = get_total_by_brand_and_type(conn)
        df = pd.DataFrame(df)
        return df
        conn.close()

def get_in4code(code):
    conn = create_connection()
    if conn:
        df = get_in4Product_byCode(code)
        df = pd.DataFrame(df)
        # print(df)
        return df
        conn.close()

# Ví dụ sử dụng

# if __name__ == "__main__":
#     product_in4 = get_in4Product_byCode("A002")
#     if product_in4:
#         print(product_in4)
    
