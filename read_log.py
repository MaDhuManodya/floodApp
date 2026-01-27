
try:
    count = 0
    with open('build.log', 'r', encoding='utf-8', errors='replace') as f:
        for i, line in enumerate(f):
            if ': error' in line or ': Error' in line:
                print(f"{i}: {line.strip()}")
                count += 1
    if count == 0:
        print("No errors found in utf-8 mode, trying utf-16")
        with open('build.log', 'r', encoding='utf-16', errors='replace') as f:
            for i, line in enumerate(f):
                if ': error' in line or ': Error' in line:
                    print(f"{i}: {line.strip()}")
except Exception as e:
    print(f"Failed: {e}")
