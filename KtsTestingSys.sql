
CREATE TABLE [Roles]
(
	[role_id] int  PRIMARY KEY identity(1,1) NOT NULL, 
    [description] VARCHAR(128) not NULL    
)

insert into roles (description) values ('data entry');
insert into roles (description) values ('test compiler');
insert into roles (description) values ('admin');
insert into roles (description) values ('library management');

CREATE TABLE [Departments]
(
	[department_id] bigint  PRIMARY KEY identity(1,1) NOT NULL, 
    [description] VARCHAR(128) not NULL, 
	[user_Id] BIGINT,
	[insertion_timestamp] DATETIME2 Not NULL DEFAULT getdate()	
)


CREATE TABLE [Users]
(
	[user_Id] BIGINT  PRIMARY KEY identity(1,1) NOT NULL, 
    [first_names] VARCHAR(128) , 
    [last_name] VARCHAR(128) NOT NULL, 
    [username] VARCHAR(128) NOT NULL, 
    [password] VARCHAR(256) NOT NULL, 
	[designation] VARCHAR(64) , 
    [status] VARCHAR(32) NOT NULL DEFAULT 'active', 
	[parent_department_id] BIGINT references Departments, 
    --[user_type_id] INT NOT NULL references User_types, 
    [creation_user_id] BIGINT , 
    [insertion_timestamp] DATETIME2 Not NULL DEFAULT getdate()	
)
insert into users  (first_names,last_name,username,password) values ('super','admin','system','65cb13fa01cc09668b6b02eae57ead0d');

create table user_role_map
(
	user_id  bigint references users,
	role_id int references roles,
	constraint pk_user_role_map primary key CLUSTERED  (user_id,role_id)
)
insert into user_role_map (user_Id,role_id) values (1,1);
insert into user_role_map (user_Id,role_id) values (1,2);
insert into user_role_map (user_Id,role_id) values (1,3);
insert into user_role_map (user_Id,role_id) values (1,4);

CREATE TABLE subjects
(
	subject_id bigint  PRIMARY KEY identity(1,1) NOT NULL, 
    [description] VARCHAR(128) not NULL    
)

CREATE TABLE question_levels
(
	level_id int  PRIMARY KEY identity(1,1) NOT NULL, 
    [description] VARCHAR(128) not NULL    
)

CREATE TABLE question_difficulty
(
	difficulty_code varchar(32)  PRIMARY KEY  NOT NULL,
	[description] VARCHAR(128) not NULL  
)
insert into question_difficulty (difficulty_code,description) values ('easy','easy');
insert into question_difficulty (difficulty_code,description) values ('moderate','moderate');
insert into question_difficulty (difficulty_code,description) values ('difficult','difficult');

CREATE TABLE question_importance
(
	importance_code varchar(32)  PRIMARY KEY  NOT NULL,
	[description] VARCHAR(128) not NULL  
)
insert into question_importance (importance_code,description) values ('essential','essential');
insert into question_importance (importance_code,description) values ('important','important');
insert into question_importance (importance_code,description) values ('supplementary','supplementary');

CREATE TABLE questions
(
	question_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,
	description	varchar(256) not null,
	subject_id	bigint not null references subjects,
	difficulty_code varchar(32) references  question_difficulty,
	importance_code varchar(32) references  question_importance,
	marks	int not null default 1,
	question_level_id int references question_levels,
	user_Id		bigint not null references users,
	[insertion_timestamp] DATETIME2 Not NULL DEFAULT getdate()	
)

CREATE TABLE question_images
(
	question_id bigint  PRIMARY KEY references questions, 
    image_data image not null    
)

CREATE TABLE answers
(
	answer_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,
	question_id bigint references questions not null,
	description	varchar(256) not null,
	correct_p	bit not null
)

CREATE TABLE answer_images
(
	answer_id bigint  PRIMARY KEY references answers, 
    image_data image not null    
)

CREATE TABLE User_Tests
(
	test_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,
    [description] VARCHAR(128) not NULL,    
	question_level_id int references question_levels,
	total_questions int not null default 10,
	total_time_minutes	int not null default 10,
	total_marks int not null default 10,
	status	varchar(16) not null default 'draft',--ready, deleted
	test_versions int not null,
	test_creator varchar(64),
	user_Id		bigint not null references users,
	[insertion_timestamp] DATETIME2 Not NULL DEFAULT getdate()
)
/*
CREATE TABLE User_Test_Filters
(
	user_test_filter_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,	
	test_id bigint references User_Tests,
	subject_id	bigint not null references subjects,
	difficulty_code varchar(32) references  question_difficulty,
	importance_code varchar(32) references  question_importance,	 
	no_of_questions int not null default 1,
)*/
CREATE TABLE user_test_subjects
(
	user_test_subject_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,	
	test_id bigint references User_Tests,
	subject_id	bigint not null references subjects,	 
	no_of_questions int not null default 1
)

CREATE TABLE test_subjects_difficulty
(
	test_subject_difficulty_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,	
	user_test_subject_id bigint references user_test_subjects,
	difficulty_code varchar(32) references  question_difficulty,	 
	no_of_questions int not null default 1
)
CREATE TABLE subject_difficulty_importance
(
	difficulty_importance_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,	
	test_subject_difficulty_id bigint references test_subjects_difficulty,
	importance_code varchar(32) references  question_importance, 
	no_of_questions int not null default 1
)
CREATE TABLE Test_questions
(	
	test_question_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,	
	test_id bigint  references User_Tests,
	question_id bigint references questions,
	--version_number int not null,
)

CREATE TABLE Test_versions
(	
	test_version_id bigint  PRIMARY KEY  identity(1,1) NOT NULL,	
	version_number int not null,
	test_question_id bigint  references Test_questions,
	test_id bigint references User_Tests
)
