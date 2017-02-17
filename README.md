# SKEhealthAdapter
Repository obsahuje implementáciu adaptéra pre komunikáciu s centrálnym systémom projektu eHealth - http://www.ezdravotnictvo.sk

Projekt obsahuje 2 varianty:
* Verzia vyuzivajuca CryptoController dodavany v ramci projektu eHealth - pre kompilaciu tejto verzie je nevyhnutne umiestnit subory cryptocontroller do adreasara External\CC
* Verzia ktora obsahuje kompletnu implementaciu (**verzia nepodporuje OS Windows XP**)

---
Projekt je vytvorený v rámci MS Visual Studio 2015.

Alternatívne je možné vytvoriť kompilovanú verziu pomocou priložených skriptov 
* 'compile.cmd'
* 'compileCC.cmd'
Skripty pre svoj beh vyzaduju instalaciu .net frameworku 4 a su pripravene pre x64 operacny system. Pre ine prostredie mozu byt potrebne upravy.