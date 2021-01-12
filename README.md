# eHome

## Avtorja
 - Denis Feratović, 63170087, df1538@student.uni-lj.si
 - Jure Časar, 63180069, jc2484@student.uni-lj.si

## Android aplikacija
![](https://github.com/Gnortss/ehome/blob/master/slika_1.png)
![](https://github.com/Gnortss/ehome/blob/master/slika_2.png)


## Opis Informacijskega Sistema
Informacijski sistem vsebuje spletno aplikacijo, podatkovno bazo in android odjemalca.
Spletna aplikacija in podatkovna baza sta gostovani na Azure portalu.

Na spletni aplikaciji lahko uporabniki iščejo in ustvarjajo oglase za nepremičnine. Za ustvarjanje oglasov je potrebna avtentikacija. Uporabniki lahko filtrirajo po oglasih ter jih dodajajo med priljubljene. Admin uporabnik ima nadzor nad vsemi uporabniki in njihovimi nepremičninami. Te lahko briše ali spreminja.

Spletna aplikacija implementira tudi REST Api, ki je potem uporabljen pri Android odjemalcu.

Na Android odjemalcu lahko uporabnik izvede dva Api klica za seznam nepremičnin (GET, CREATE). Na prvi strani se mu avtomatsko prikažejo vsi oglasi nepremičnin. Nepremičnino pa lahko tudi ustvari.

## Opis nalog
### Spletna aplikacija
- Kreiranje podatkovnega modela - Denis, Jure
- Kreiranje osnovnih mvc kontrolerjev - Denis, Jure
- Grafični vmesnik - Denis
- Avtentikacija - Jure
- Filtri za nepremičnine - Denis
- Navigacija po spletni strani - Denis
- REST Api - Jure
- REST Api dokumentacija - Denis
- Gostovanje podatkovne baze in spletne aplikacije - Jure
### Android odjemalec
- GET zahtevek - Denis
- POST zahtevek - Jure
- Grafični vmesnik - Denis

## Podatkovni model
![](https://github.com/Gnortss/ehome/blob/master/slika_3.png)

### Domain description
Create a web application, where users can advertise or search for Real Estate.
Anyone on the website will be able to search for listings using the Filters provided.
For Users to be able to create Listings and add their Favorite listings they will have to
create an Account. If a user is logged in, they will also have full control over their Listings(edit, delete). 

## Start developing

### Reset database and migrations
 - Drop DB ```dotnet ef database drop```
 - Remove Migrations ```dotnet ef migrations remove```
 - Add Migration ```dotnet ef migrations add Start```
 - Apply migrations ```dotnet ef database update```

### 1st Time
 - Clone ```git clone git@github.com:Gnortss/ehome.git```
### Later
 - Checkout master ```git checkout master```
 - Pull Changes ```git pull```
 - Create feature branch ```git checkout -b <feature_branch>```
 - Adding and commiting changes ```git add . && git commit -m "my changes"```
 - Push to remote ```git push -u origin <feature_branch>```
 - Create pull request on github page
 - Merge if no conflicts