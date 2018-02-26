# HairSalon WebPage

#### This is a WebPage to display stylists to customers and help employee's management.

#### By Qianqian Hu on 2018/01/25

## Description

This webpage will help employee manage data of stylists and customers.

## Setup/Installation Requirements

* Copy repository from GitHub to your computer using Terminal command $ git clone https://github.com/QIANQIANHU/HairSalon
* Download and install MAMP refering to https://www.learnhowtoprogram.com/c/database-basics-ee7c9fd3-fcd9-4fff-8b1d-5ff7bfcbf8f0/introducing-and-installing-mamp
* Ensure our MAMP server is running;
* Run Terminal command $ /Applications/MAMP/Library/bin/mysql --host=localhost -uroot -proot;/
* Check the Apache Server and MySQL Sever checkboxes in the upper-left corner of the MAMP window.
    - CREATE DATABASE qianqian_hu;
    - USE qianqian_hu;
    - CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
    - CREATE TABLE customers (id serial PRIMARY KEY, description VARCHAR(255));

* Run with "dotnet run" command line on your Terminal;
* Copy and paste the link(http://localhost:5000) to your browser;
* Note: uploaded DBs are in zipped compression format.

## Technologies Used

* C#
* MAMP
* .NET CORE
* HTML
* CSS


## Support and contact details

Contact email: huqianqian@ymail.com

### License

Copyright (c) 2018 **_{Qianqian Hu}_**Permission is hereby granted, free of charge, to any person obtaining_
a copy of this software and associated documentation files (the "Software"), to deal in the Software without_
restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,_
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the_
following conditions:_The above or foregoing copyright notice and this permission notice shall be included in all copies_
or substantial portions of the Software.__THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,_
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT._
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN_
ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS_
IN THE SOFTWARE._
