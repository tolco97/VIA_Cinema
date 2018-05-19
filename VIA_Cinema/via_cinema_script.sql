create table if not exists via_cinema_schema.movies
(
	name varchar(250) not null
		constraint movies_pkey
			primary key,
	duration_minuites integer,
	genre varchar(50)
);

create table if not exists via_cinema_schema.user_accounts
(
	email varchar(50) not null
		constraint user_accounts_pkey
			primary key,
	password varchar(50),
	first_name varchar(250),
	last_name varchar(250),
	birthday timestamp
);

create table if not exists via_cinema_schema.projections
(
	id serial not null
		constraint projections_pkey
			primary key,
	movie_name varchar(250)
		constraint projections_movie_name_fkey
			references movies
				on update cascade on delete set null,
	projection_start timestamp
);

create table if not exists via_cinema_schema.seat_reservations
(
	projection_id integer not null
		constraint seat_reservations_projection_id_fkey
			references projections
				on update cascade on delete set null,
	email varchar(50) not null
		constraint seat_reservations_email_fkey
			references user_accounts
				on update cascade on delete set null,
	seat_number integer not null,
	constraint seat_reservations_pkey
		primary key (projection_id, email, seat_number)
);
