# defrag

Ceci est un faux d�fragmenteur de disque pour Windows qui reprend l'interface du vrai d�fragmenteur de Windows 95/98 (certaines fonctionnalit�s ne sont toutefois pas compl�tement identiques)

## principe de fonctionnement

L'affichage des blocs ainsi que le pourcentage d'avancement sont al�atoires. De la m�me fa�on, l'utilisateur re�oit al�atoirement un message visuel (non personnalisable) bas� sur une disposition particuli�re des blocs  

L'application s'ex�cute tant que l'utilisateur ne met pas en pause ni n'arr�te la d�fragmentation

## utilisation de l'interface graphique

### 1. nouvelle d�fragmentation

La d�fragmentation est lanc�e au d�marrage de l'application et le lecteur C: est arbitrairement choisi

### 2. pause

Un clic sur le bouton "Pause" met la d�fragmentation en pause. Le texte du bouton devient "Reprendre". Cliquer une nouvelle fois pour reprendre

### 3. arr�t

Un clic sur le bouton "Arr�t" arr�te la d�fragmentation (sans possibilit� de reprise). L'utilisateur a aussi la possibilit� de fermer la fen�tre

### 4. l�gende

Un clic sur le bouton "L�gende" affiche une fen�tre pop-up affichant le code couleur pr�cis� dans le paragraphe 5 de la section "utilisation de l'interface graphique". Cliquer une nouvelle fois pour la faire dispara�tre

### 5. d�tails

Un clic sur le bouton "D�tails" cache la progression de la d�fragmentation. Cliquer une nouvelle fois pour l'afficher

## code couleur

| couleur du bloc | signification |
| ----------- | ----------- |
| blanc | libre |
| rouge | d�fectueux |
| bleu | d�fragment� |
| turquoise | fragment� |
| noir | inamovible |