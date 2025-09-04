# Commit Message Policy

To ensure clear traceability, every commit message must follow a structured format and include a valid Ticket ID.

---

## Commit Message Format

Required Format:

```  
QTD2-####

Summary:
Brief one-line explanation of the change.

Description:
- What was changed and why
- What files/modules were affected
- Any relevant implementation notes

Test Instructions:
- How a reviewer or QA can test this change
- Any environment setup or data needed  
```

---

## Example

```  
QTD2-1042

Summary:
Fix login null reference issue

Description:
Ensures user is initialized in AuthService before login attempt.
Prevents edge case where session expiry crashes login form.

Test Instructions:
1. Logout and load login page
2. Wait 15+ minutes
3. Attempt login — it should succeed without error  
```

---

## Acceptable Exceptions

If your commit doesn't relate to a ticket, use one of these allowed prefixes:

| Prefix       | When to Use                  |
|--------------|------------------------------|
| NO-TICKET    | Emergency or meta changes    |
| INIT         | Initial commit / setup       |
| DOCS         | Documentation-only commits   |

Example:

```  
NO-TICKET

Summary:
Update README formatting  
```

---

## Commit Hook Setup (Local Only)

To enforce the format, install a Git commit hook.

### Step 1: Create the Hook

Create a file in your repo at:

```
.git/hooks/commit-msg
```

Paste the following contents into that file:

```bash
#!/bin/sh

if grep -qE "^(QTD2-[0-9]+|NO-TICKET|INIT|DOCS)" "$1"; then
  exit 0
fi

echo "ERROR: Commit message must start with 'QTD2-####' or an accepted exception (NO-TICKET, INIT, DOCS)"
exit 1
```

### Step 2: Make It Executable

On Git Bash or terminal:

```
chmod +x .git/hooks/commit-msg
```

This hook is local to your machine. Each developer must install it manually unless automated via script or a Git hook manager.

---

## Skipping the Hook (Not Recommended)

To skip the commit hook during a one-time commit:

```
git commit --no-verify
```

This should only be done when absolutely necessary and with proper context in the commit message.
