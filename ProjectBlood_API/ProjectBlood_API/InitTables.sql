CREATE TABLE IF NOT EXISTS Users(	
	customid bigint primary key not null,
	bloodtype varchar(3) not null,
	name text not null,
	surname text not null,
	birthdate date not null
);

CREATE TABLE IF NOT EXISTS Contacts(	
	customid bigint primary key not null,
	cities text not null,
	phonenumber text not null,
	email text not null,
	constraint fk_userid foreign key (customid) references Users (customid)
);