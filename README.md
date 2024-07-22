# defrag

Ceci est un faux défragmenteur de disque pour Windows qui reprend l'interface du vrai défragmenteur de Windows 95/98 (certaines fonctionnalités ne sont toutefois pas complètement identiques)

## principe de fonctionnement

L'affichage des blocs ainsi que le pourcentage d'avancement sont aléatoires. De la même façon, l'utilisateur reçoit aléatoirement un message visuel (non personnalisable) basé sur une disposition particulière des blocs  

L'application s'exécute tant que l'utilisateur ne met pas en pause ni n'arrête la défragmentation

## utilisation de l'interface graphique

### 1. nouvelle défragmentation

La défragmentation est lancée au démarrage de l'application et le lecteur C: est arbitrairement choisi

### 2. pause

Un clic sur le bouton "Pause" met la défragmentation en pause. Le texte du bouton devient "Reprendre". Cliquer une nouvelle fois pour reprendre

### 3. arrêt

Un clic sur le bouton "Arrêt" arrête la défragmentation (sans possibilité de reprise). L'utilisateur a aussi la possibilité de fermer la fenêtre

### 4. légende

Un clic sur le bouton "Légende" affiche une fenêtre pop-up affichant le code couleur précisé dans la section "[code couleur](#code-couleur)". Cliquer une nouvelle fois pour la faire disparaître

### 5. détails

Un clic sur le bouton "Détails" cache la progression de la défragmentation. Cliquer une nouvelle fois pour l'afficher

### 6. paramètres

Un clic sur le bouton "Paramètres" affiche les paramètres de l'application

#### 1. délai du message à l'utilisateur

Il s'agit de l'intervalle de temps entre deux messages à destination de l'utilisateur (évoqués dans la section "[principe de fonctionnement](#principe-de-fonctionnement)"). Exprimé en millisecondes, c'est un nombre entier entre 0 et 99999

## code couleur

| couleur du bloc | signification |
| ----------- | ----------- |
| blanc | libre |
| rouge | défectueux |
| bleu | défragmenté |
| turquoise | fragmenté |
| noir | inamovible |