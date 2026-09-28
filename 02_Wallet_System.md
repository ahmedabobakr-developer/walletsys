# Wallet System — Mini Project Specification

## 1. فكرة المشروع

نبني Wallet صغيرة تعتمد على Balance + Transaction History.

الـWallet ليست Payment Gateway ولا نظام بنكي. هي Feature يمكن استخدامها داخل:
- Rewards System
- Marketplace
- Loyalty Platform
- Gaming
- Subscription/Product platform

الهدف الأساسي هو إبراز الـBackend Business Logic وData Integrity.

## 2. الهدف من المشروع

Demo:

```text
┌──────────────────────────┐
│       MY WALLET          │
│                          │
│       1,250 pts          │
│                          │
│   [+ Credit] [- Debit]   │
└──────────────────────────┘

Transactions
+500  Reward
-200  Redeemed
+100  Bonus
```

## 3. Scope — MVP فقط

### User Story 1 — View Wallet
المستخدم يرى الـCurrent Balance.

### User Story 2 — Credit
إضافة Points إلى الـWallet.

### User Story 3 — Debit
خصم Points من الـWallet.

### User Story 4 — Transaction History
كل حركة مالية/نقطية يتم تسجيلها.

### User Story 5 — Prevent Invalid Debit
لا يمكن خصم قيمة أكبر من الرصيد.

## 4. Data Model

نحتاج كيانين فقط.

### Wallet

```text
Wallet
------
Id
UserId
Balance
CreatedAt
UpdatedAt
```

### WalletTransaction

```text
WalletTransaction
-----------------
Id
WalletId
Type
Amount
BalanceAfter
Reason
CreatedAt
```

`Type`:

```text
Credit
Debit
```

`BalanceAfter` مهم لأنه يجعل كل Transaction تحمل snapshot للرصيد بعدها.

## 5. Business Rules

### Credit

```text
Current Balance = 1000
Credit = 250

New Balance = 1250
```

### Debit

```text
Current Balance = 1000
Debit = 300

New Balance = 700
```

### Invalid Debit

```text
Current Balance = 1000
Debit = 1500

❌ Insufficient Balance
```

### Transaction

كل عملية ناجحة يجب أن:

```text
Update Wallet Balance
        +
Create WalletTransaction
```

ويجب أن تتم العملية كـatomic database operation.

## 6. API Contract

```http
GET  /api/wallet
GET  /api/wallet/transactions
POST /api/wallet/credit
POST /api/wallet/debit
```

Request مثال:

```json
{
  "amount": 250,
  "reason": "Weekly Reward"
}
```

## 7. Important Backend Point

لا تعتمد على الـAngular في حماية الـBalance.

الـBackend هو المسؤول عن:

```text
Validate amount
↓
Check balance
↓
Update wallet
↓
Create transaction
```

والـUI مجرد client.

لو بقي وقت، يمكن إضافة optimistic concurrency، لكن **ليس جزءًا إلزاميًا من الـ3-hour MVP**.

## 8. Angular UI

### Balance Card

```text
Current Balance

1,250 pts
```

### Actions

```text
[ + Credit ] [ - Debit ]
```

### Transaction List

```text
+500   Weekly Reward       1,250
-200   Redeemed            750
+100   Bonus               950
```

نستخدم visual distinction بين Credit وDebit بدون بناء Dashboard ضخم.

## 9. Validation

Frontend:

- Amount > 0
- Reason required

Backend:

- Amount > 0
- Wallet exists
- User owns wallet
- Debit <= balance

الـBackend validation هي الأساس حتى لو تم تجاوز الـAngular.

## 10. ما لن نبنيه

حتى لا يتضخم المشروع:

- Payment Gateway
- Stripe/PayPal
- Bank integration
- Transfers between users
- Multiple currencies
- Refund system
- Withdrawal
- Authentication/Authorization كامل
- Complex accounting
- Admin dashboard
- Distributed locking

## 11. Time Budget — Maximum 3 Hours

### 0:00–0:20 — Setup
- .NET API
- Angular
- EF Core
- Database

### 0:20–0:50 — Database
- Wallet
- WalletTransaction
- Relationships
- Migration
- Seed Wallet

### 0:50–1:35 — Core Backend
- Get wallet
- Get transactions
- Credit
- Debit
- Validation
- Business logic

### 1:35–1:55 — Atomic Operation
- Transaction scope/database transaction
- Ensure Wallet update + Transaction insert succeed together

### 1:55–2:35 — Angular
- Balance card
- Credit form
- Debit form
- Transactions list

### 2:35–3:00 — Polish + Testing
اختبر:

```text
1000
+500
= 1500

1500
-300
= 1200

1200
-2000
= ❌ rejected
```

ثم README وDemo.

## 12. Definition of Done

- Balance يظهر بشكل صحيح.
- Credit يعمل.
- Debit يعمل.
- Invalid debit يتم رفضه من Backend.
- كل عملية ناجحة لها Transaction.
- BalanceAfter صحيح.
- Wallet update وTransaction insert يتمان بشكل atomic.
- Angular يعرض النتيجة بدون تعقيد.
- README يشرح الـbusiness rules.

## 13. Portfolio Talking Points

> Built a wallet and transaction module with ASP.NET Core and EF Core, implementing business rules, atomic balance updates, transaction history, and insufficient-balance validation.

أهم شيء في الـPortfolio هنا ليس شكل الـWallet، بل قدرتك على شرح:

> "ليه العملية لازم تكون Atomic؟ وإيه اللي يحصل لو تحديث الرصيد نجح وتسجيل الـTransaction فشل؟"

