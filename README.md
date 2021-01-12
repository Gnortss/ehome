# eHome

### Avtorja
 - Denis Feratović, 63170087, df1538@student.uni-lj.si
 - Jure Časar, 63180069, jc2484@student.uni-lj.si

### Android aplikacija
![]

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