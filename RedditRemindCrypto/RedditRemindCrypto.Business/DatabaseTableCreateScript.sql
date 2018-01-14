
CREATE TABLE RemindRequests
(
    Id uniqueidentifier NOT NULL DEFAULT NEWID() PRIMARY KEY,
    Expression nvarchar(255) NOT NULL,
    [User] nvarchar(100) NOT NULL,
    Permalink nvarchar(100) NULL
)

CREATE TABLE Currencies
(
    Ticker nvarchar(10) NOT NULL PRIMARY KEY,
    CurrencyType int NOT NULL,
    FixerIOName nvarchar(100) NULL,
    CoinMarketCapId nvarchar(100) NULL
)

CREATE TABLE CurrencyAlternativeNames
(
    Name nvarchar(100) NOT NULL PRIMARY KEY,
    CurrencyTicker nvarchar(10) REFERENCES Currencies(Ticker) 
)

-- EUR
INSERT INTO Currencies (CurrencyType, Ticker, FixerIOName) VALUES (1, 'EUR', 'EUR')
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('€', (SELECT Ticker FROM Currencies WHERE Ticker = 'EUR'))
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('euro', (SELECT Ticker FROM Currencies WHERE Ticker = 'EUR'))

-- USD
INSERT INTO Currencies (CurrencyType, Ticker, FixerIOName) VALUES (1, 'USD', 'USD')
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('$', (SELECT Ticker FROM Currencies WHERE Ticker = 'USD'))
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('dollar', (SELECT Ticker FROM Currencies WHERE Ticker = 'USD'))

-- GBP
INSERT INTO Currencies (CurrencyType, Ticker, FixerIOName) VALUES (1, 'GBP', 'GBP')
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('£', (SELECT Ticker FROM Currencies WHERE Ticker = 'GBP'))
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('pound', (SELECT Ticker FROM Currencies WHERE Ticker = 'GBP'))

-- JPY
INSERT INTO Currencies (CurrencyType, Ticker, FixerIOName) VALUES (1, 'JPY', 'JPY')
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('¥', (SELECT Ticker FROM Currencies WHERE Ticker = 'JPY'))
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('yen', (SELECT Ticker FROM Currencies WHERE Ticker = 'JPY'))


-- BTC
INSERT INTO Currencies (CurrencyType, Ticker, CoinMarketCapId) VALUES (2, 'BTC', 'bitcoin')
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('bitcoin', (SELECT Ticker FROM Currencies WHERE Ticker = 'BTC'))

-- BCH
INSERT INTO Currencies (CurrencyType, Ticker, CoinMarketCapId) VALUES (2, 'BCH', 'bitcoin-cash')
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('bitcoincash', (SELECT Ticker FROM Currencies WHERE Ticker = 'BCH'))
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('bitcoin-cash', (SELECT Ticker FROM Currencies WHERE Ticker = 'BCH'))

-- LTC
INSERT INTO Currencies (CurrencyType, Ticker, CoinMarketCapId) VALUES (2, 'LTC', 'litecoin')
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('litecoin', (SELECT Ticker FROM Currencies WHERE Ticker = 'LTC'))

-- VTC
INSERT INTO Currencies (CurrencyType, Ticker, CoinMarketCapId) VALUES (2, 'VTC', 'vertcoin')
INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('vertcoin', (SELECT Ticker FROM Currencies WHERE Ticker = 'VTC'))