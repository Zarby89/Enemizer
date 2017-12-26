VitreousKeyReset:
    STZ $0CBA, X
    JSL $0DB818 ;restore old code
RTL
