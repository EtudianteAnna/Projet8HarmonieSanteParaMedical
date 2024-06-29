# Projet8HarmonieSanteParaMedical
## 1. Aperçu du projet
### 1.1 Objectifs du projet
- Migrer vers une architecture de microservices
- Optimiser la gestion des rendez-vous et résoudre les conflits d'horaires
- Améliorer les performances et la scalabilité de l'application
- Renforcer la sécurité des accès

### 1.2 Points non couverts par le projet
- Système complet d'authentification et d'autorisation
- Gestion avancée des événements pour les praticiens
- Implémentation d'une passerelle API complète

### 1.3 Métriques du projet
- Temps de chargement de l'application et de l'agenda
- Absence de chevauchements dans l'agenda
- Précision des données affichées dans l'agenda

## 2. Caractéristiques
- Connexion et inscription des utilisateurs
- Gestion personnalisée de l'agenda
- Prise et annulation de rendez-vous
- Profils utilisateurs (patients et praticiens)
- Espace dédié aux praticiens

## 3. Spécifications techniques
### 3.1 Technologies utilisées
- Backend : ASP.NET Core 6 (MVC et API)
- Frontend : JavaScript pour les communications API
- Passerelle API : Ocelot
- Authentification : Microsoft Identity et JWT
- ORM : Entity Framework Core
- Base de données : SQL Server
- Tests : xUnit pour les tests unitaires et d'intégration
- Tests de charge : JMeter

### 3.2 Architecture
- Service d'authentification
- Service de gestion des praticiens
- Service de gestion de l'agenda
- Application Web principale (MVC)

### 3.3 Tests
- Tests unitaires pour les services API (praticiens et agenda)
- Tests de charge pour vérifier la performance avec 3000 requêtes simultanées

### 3.4 Solutions à long terme
- Isolation du serveur d'identité dans un service distinct
- Ajout d'un service de paiement
- Conteneurisation avec Docker

## 4. Installation et déploiement
[Instructions pour installer et déployer l'application]

## 5. Utilisation
[Guide rapide sur l'utilisation de l'application]

## 6. Contribution
[Instructions pour les contributeurs potentiels]

## 7. Licence
[Informations sur la licence du projet]

## 8. Contact
[Informations de contact pour le support ou les questions]




