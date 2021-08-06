from snoop import pp
import better_exceptions

users = { 
    'user1': { 'is_admin': True, 'email': 'one@exmple.com'},
    'user2': { 'is_admin': True, 'phone': '281-555-5555' },
    'user3': { 'is_admin': False, 'email': 'three@example.com' },
}

def email_user(*user_names) -> None:
    global users
    for user in user_names:
        print("Emailing %s at %s", (user, users[user]['email']))

bank_accounts = [0, 100.5]
def deposit_money(amount: float, account_number: int) -> None:
    global bank_accounts, users
    bank_accounts[account_number-1] += amount
    # if over $10k, notifiy admins
    if amount >= 10_000:
        admins = [u for u in users if users[u]['is_admin']]
        email_user(*admins)


deposit_money(1_000, 1)
pp(bank_accounts)
deposit_money(11_100, 1)