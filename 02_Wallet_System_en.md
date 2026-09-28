# Wallet System — Mini Project Specification

## 1. Project Overview

We are building a lightweight Wallet module focused on Balance management and Transaction History.

This wallet is not a Payment Gateway or a core banking engine. It serves as an embedded internal ledger feature for:

- Rewards & Loyalty platforms
- In-app credits / Marketplaces
- Gaming systems
- Subscription / Product credit mechanisms

The core objective is to demonstrate solid Backend Business Logic, Transaction Isolation, and Data Integrity.

---

## 2. Project Goal & Demo

```text
┌──────────────────────────┐
│       MY WALLET          │
│                          │
│       1,250 pts          │
│                          │
│   [+ Credit] [- Debit]   │
└──────────────────────────┘
```

Transactions

```text
+500  Reward
-200  Redeemed
+100  Bonus
```

---

## 3. Scope — MVP Only

- **User Story 1 — View Wallet**: The user views their current wallet balance.
- **User Story 2 — Credit**: Add funds/points to the wallet.
- **User Story 3 — Debit**: Deduct funds/points from the wallet.
- **User Story 4 — Transaction History**: Every financial or point event is immutably recorded.
- **User Story 5 — Prevent Invalid Debit**: The backend strictly prevents deducting an amount greater than the current balance.

---

## 4. Data Model

### Wallet

```text
Wallet
------

Id          (Guid / int)
UserId      (string / Guid)
Balance     (decimal)
CreatedAt   (DateTime)
UpdatedAt   (DateTime)
```

### WalletTransaction

```text
WalletTransaction
-----------------

Id             (Guid / int)
WalletId       (Guid / int)
Type           (Credit | Debit)
Amount         (decimal)
BalanceAfter   (decimal)
Reason         (string)
CreatedAt      (DateTime)
```

> **Note**: `BalanceAfter` is critical because it stores a snapshot of the running balance immediately following the transaction, providing an audit trail.

---

## 5. Business Rules & Logic

### Credit Flow

```text
Current Balance = 1000

Credit = 250

New Balance = 1250
```

### Debit Flow

```text
Current Balance = 1000

Debit = 300

New Balance = 700
```

### Invalid Debit Flow

```text
Current Balance = 1000

Debit = 1500

Result: ❌ Insufficient Balance (400 Bad Request)
```

### Atomic Consistency Rule

Every successful balance mutation must execute as an atomic database transaction:

```text
Update Wallet Balance
        +
Create WalletTransaction Record
```

Both operations must succeed or roll back together.

---

## 6. API Contract

```text
GET  /api/wallet

GET  /api/wallet/transactions

POST /api/wallet/credit

POST /api/wallet/debit
```

**Payload Example:**

```json
{
  "amount": 250,
  "reason": "Weekly Reward"
}
```

---

## 7. Angular UI Components

1. **Balance Card**: Renders the current balance prominently (`1,250 pts`).
2. **Action Controls**: Simple trigger forms for `[+ Credit]` and `[- Debit]` with amount and reason fields.
3. **Transaction History List**:
   - Distinct visual cues (green for credit, red for debit).
   - Columns: Type, Reason, Amount, and Running Balance snapshot.

---

## 8. Validation Strategy

### Frontend:

- Amount > 0.
- Reason field is non-empty.

### Backend (Source of Truth):

- Amount > 0.
- Target wallet exists and belongs to the authenticated user.
- Debit amount <= current balance.

---

## 9. Out of Scope

To prevent scope creep:

- External Payment Gateways (Stripe, PayPal)
- Direct peer-to-peer user transfers
- Multi-currency conversions
- Complex double-entry accounting ledgers
- Admin management dashboards

---

## 10. Time Budget (Max 3 Hours)

- **0:00–0:20 — Setup**: .NET Web API, Angular workspace, EF Core setup.
- **0:20–0:50 — Database**: Entities, relationships, migrations, seed default wallet.
- **0:50–1:35 — Core Backend**: Endpoints, DTOs, validations, domain rules.
- **1:35–1:55 — Atomic Transaction**: Implementation of `BeginTransactionAsync()` or transaction scope.
- **1:55–2:35 — Angular**: Balance display, credit/debit forms, history table.
- **2:35–3:00 — Polish & Testing**: Negative balance testing, concurrency verification, README.

---

## 11. Portfolio Talking Points

> "Engineered an in-app wallet and transaction ledger using ASP.NET Core and EF Core. Guaranteed ledger integrity by enforcing atomic database transactions and strict server-side validation to prevent race conditions and overdrafts."