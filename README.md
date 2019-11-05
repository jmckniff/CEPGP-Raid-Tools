## CEPGP Raid Tools

This is a web application which can be used to display World of Warcraft EPGP standings and transactions exported from the [CEPGP addon](https://github.com/Alumian/CEPGP).

### Features

#### General 
* View guild member EPGP standings.
* View EPGP transactions.
* View EPGP transactions for a specific Guild member.
* View guild member raid attendance.

#### Admin Section
* Upload EPGP standings exported from CEPGP "/Admin/Standings"
* Upload EPGP transaction exported from CEPGP. "/Admin/Transactions"

#### API 
* JSON endpoint to get all guild member EPGP standings "/API/Standings"
* JSON endpoint to get all EPGP transactions "/API/Transactions"
* JSON endpoint to get EPGP transactions for a specific guild member "/API/Transactions/Member?username=Chobo"
