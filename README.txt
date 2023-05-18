# FinancialAccountingTest

FinancialAccountingTest is an app, made for our hackaton-group pre-examination purpose.
Main functionality is developed to allow simple financial accounting, and per-month financial reporing.
The app also allows us getting real-time state of currency convertion price, using PrivatBankAPI.

## Installation

App is being distributed as an exe-type file, that is ready to execution, without installation requirement.

## Usage

When the app is being started, you face greeting screen, when you have only one button-type control.
You need to press the "Hello" button, so that database connection will be established. This screen is
created to catch database exceptions on button click, and allow user understand, what is going on with
the app.
In case you press greeting button, you will then be guided to main app-management screen. On this screen
you are able to check for all of your financial status components. Those financial status components can be
such: Income/Spending/Deposit/Credit. Income is one-time addition to your financial status, 
Spending - one-time substraction from your financial status, Deposit - periodic addition to your financial
status, Credit - periodic substraction from your financial status.
On window opened, you are able to montior real-time state of foreign currency price. The feature is connected
to PrivatBankAPI.
Also AdminWindow helps you reach CRUD for the whole database. Remove feature is being applied, with respect
to the current table selection. Add and Edit features, on the other hand, are opening separate forms, where
you can fill form, and choose, whether you are willing to change database, or get back to AdminWindow overview.
Remove feature is cautious, and asks you twice, in case you are attempting database deletion.
The app has a separate report window. That report is being formed per-month. It first sums up all of
occasional logs month-by-month, and then, taking advantage of Date property fields, forms whole report.
Data received is being displayed in chart form. Here, you can see, what is your total income in every month
involved.
[Warning]: Credit- and Deposit-type logs are being applied only to month, they are opened in, and forward.
All logs, that are chronologically before them, will not be changed, so Date property is essential for both
Credit- and Deposit-type logs.

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

## License
[Unilicense](https://unlicense.org)