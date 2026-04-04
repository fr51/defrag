# defrag

Ceci est un faux défragmenteur de disque pour Windows dont l'interface est inspirée du vrai défragmenteur de Windows 95/98 (certaines fonctionnalités ne sont toutefois pas complètement identiques)

Il s'agit en réalité d'un moyen déguisé de procrastination dont @[Almisuifre](https://github.com/almisuifre) m'a soufflé l'idée

## Table des matières

1. [Installation](#installation)
2. [Principe de fonctionnement](#principe-de-fonctionnement)
3. [Utilisation de l'interface graphique](#utilisation-de-linterface-graphique)
	1. [Nouvelle défragmentation](#nouvelle-défragmentation)
	2. [Arrêt](#arrêt)
	3. [Pause](#pause)
	4. [Détails](#détails)
	5. [Légende](#légende)
		1. [Code couleur](#code-couleur)
4. [Documentation](#documentation)
5. [Dépendances](#dépendances)
6. [Tests](#tests)
7. [Journal de modifications](#journal-de-modifications)
8. [Licence](#licence)

## Installation

1. Téléchargez la dernière version dans la section "Releases"

## Principe de fonctionnement

L'affichage des blocs ainsi que le pourcentage d'avancement sont aléatoires. De la même façon, l'utilisateur reçoit périodiquement un message visuel

L'application s'exécute tant que l'utilisateur ne met pas en pause ni n'arrête la défragmentation

## Utilisation de l'interface graphique

### Nouvelle défragmentation

La défragmentation est lancée au démarrage de l'application et le lecteur C est arbitrairement choisi

### Arrêt

Cliquez sur le bouton "Arrêt" pour arrêter la défragmentation sans possibilité de reprise. Vous pouvez aussi fermer la fenêtre

### Pause

Cliquez sur le bouton "Pause" pour suspendre la défragmentation. Cliquez une nouvelle fois pour reprendre

### Détails

Cliquez sur le bouton "Détails" pour cacher la progression de la défragmentation. Cliquez une nouvelle fois pour l'afficher de nouveau

###  Légende

Cliquez sur le bouton "Légende" pour afficher une fenêtre pop-up avec le [code couleur](#code-couleur) (répertorié ci-dessous) des blocs

#### Code couleur

| couleur du bloc | signification |
| ----------- | ----------- |
| blanc | libre |
| rouge | inamovible car défectueux |
| bleu | défragmenté |
| turquoise | fragmenté |
| noir | inamovible mais pas défectueux |

## Documentation

Le code est documenté en XML

## Dépendances

- .NET framework 4.8.1+. Cette application est prévue pour Windows

## Tests

Les tests sont d'ordre fonctionnel et effectués manuellement. Ils comprennent :

- la vérification du démarrage, de la suspension, de la reprise et de l'arrêt de la défragmentation
- la vérification de l'affichage/masquage des détails
- la vérification de l'affichage/masquage de la légende

## Journal de modifications

Référez-vous au [journal de modifications](changelog.md)

## Licence

Ce projet est publié sous licence Apache 2.0