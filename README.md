# Flappy-Plop  
Plop is a new tool to follow game bugs in Unity  

- liste de tous les objets
    - afficher toutes les références
- liste d’attributs pour chaque objet
- sélection des attributs qui intéressent (Code)
- sélection des attributs qui intéressent (GUI)
- ToString (XML) l’état
- Envoi de l’état par texte

    ##31-01-2016  
Utilisation réflexion pour récupérer type/nom propriétés de « MonoBehaviors »
Appui sur P pour déclencher le logging dans PlopLogs/

    ##10-02-2016
- Récupérer les valeurs des propriétés
- Voir ce que contiennent les les GameObjects

    ##29-02-2016  
Affichage des valeurs des propriétés
Messages d’erreur « OKTAMER » : accès au rigidbody impossible
Quand m’arrêter de creuser ? (Niveau 3 pour le moment)
Impossible de lire certains objets

    ##13-03-2016  
Menu implémenté

    ##27-03-2016  
Nouvelle architecture designée et implémentée

    ##15-04-2016  
Data-structure pour retenir la réflexion
Recherches Multi-threading pour écrire en parralèle, mais Unity n'accepte que des Coroutines (thread séquentiel)

    ##27-04-2016  
Restructuration code en cours
Améliorer menu

TODO : il faut faire en sorte que le menu se mette à jour si de nouveaux objets apparaissent, etc.

    ##12-05-2016  
Je me suis penché sur la mise à jour du menu :
- sélection de classes et pas d'objets 👾
- mise à jour menu en fonction de la sélection (cette partie n'est pas tout à fait fonctionnelle pour le moment) mais j'y travaille.

J'ai amélioré mon code :
- la réflexion ne se fait qu'une seule fois
- stockage dans une structure hiérarchique
- écriture dans une coroutine (thread séquencé)
=> accélère grandement l'écriture, beaucoup moins de lags et je peux générer bien plus de fichiers !

    ##15-06-2016
Recherche API Unity pour lire les rigidbody
bug des propriétés qui supprimées dans l'API refait surface avec + de propriétés...
à cause d'une restructuration d'API chez Unity : je viens de m'en rendre compte à l'instant
Comment contourner ce problème...

