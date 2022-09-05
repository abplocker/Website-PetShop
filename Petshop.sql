CREATE TABLE ADMINISTRATOR(
    admin_id VARCHAR(10) NOT NULL UNIQUE,
    name_ VARCHAR(50) not null,
    account VARCHAR(50),
    password_ varchar(50)
    CONSTRAINT pk_admin_id PRIMARY KEY(admin_id)
)

CREATE TABLE EMPLOYEE(
    employee_id VARCHAR(10) not null UNIQUE,
    full_name VARCHAR(50) not null,
    birthday DATETIME not null,
    address_ NVARCHAR(50),
    phone VARCHAR(15),
    mail VARCHAR(50),
    account VARCHAR(50),
    password_ VARCHAR(50)
    CONSTRAINT employee_id PRIMARY key(employee_id)

)

CREATE TABLE PRODUCT(
    product_id VARCHAR(10) not null UNIQUE,
    name_ NVARCHAR(50),
    type_ varchar(50),
    quantity int CHECK(quantity >= 0),
    information ntext,
    image_ image
)

CREATE TABLE ITEMS(
    items_id VARCHAR(10) not null UNIQUE,
    product_id VARCHAR(10),
    quantity int CHECK(quantity > 0),
    CONSTRAINT pk_items_id PRIMARY KEY(items_id),
    CONSTRAINT fr_product_id FOREIGN KEY(product_id) REFERENCES PRODUCT(product_id),
)

CREATE TABLE CUSTOMER(
    customer_id VARCHAR(10) not null UNIQUE,
    full_name VARCHAR(50) not null,
    birthday DATETIME not null,
    address_ NVARCHAR(50),
    phone VARCHAR(15),
    mail VARCHAR(50),
    account VARCHAR(50),
    password_ VARCHAR(50)
    CONSTRAINT pk_customer_id PRIMARY key(customer_id)
)

CREATE TABLE BILL(
    bill_id VARCHAR(10) not null UNIQUE,
    customer_id VARCHAR(10),
    items_id VARCHAR(10),
    dayBuy DATETIME,
    dayComplete DATETIME,
    dayConfirm DATETIME,
    total_payment money,
    delivery_address ntext,
    CONSTRAINT pk_bill_id PRIMARY KEY(bill_id),
    CONSTRAINT fk_customer_id FOREIGN KEY(customer_id) REFERENCES CUSTOMER(customer_id),
    CONSTRAINT fk_items_id FOREIGN KEY(items_id) REFERENCES ITEMS(items_id),
)

CREATE TABLE CART(
    cart_id VARCHAR(10) not null UNIQUE,
    customer_id VARCHAR(10),
    items_id VARCHAR(10),
    CONSTRAINT pk_cart_id PRIMARY KEY(cart_id),
    CONSTRAINT fk_cart_customer_id FOREIGN KEY(customer_id) REFERENCES CUSTOMER(customer_id),
    CONSTRAINT fk_cart_items_id FOREIGN KEY(items_id) REFERENCES ITEMS(items_id),
)

