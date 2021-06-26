/*
 * SCRIPT DE CRIAÇÃO DO BANCO
 */

-- Candidatos --
-- DROP: drop table hire_candidates;
create table hire_candidates (
  id bigserial not null,
  name varchar(100) not null,
  age int4,
  city varchar(50),
  phone varchar(20),
  email varchar(150),
  status varchar(20) not null,
  constraint hire_candidates_pk primary key (id)
);

-- Vagas
-- DROP: drop table hire_jobs;
create table hire_jobs (
  id bigserial not null,
  description varchar(255) not null,
  status varchar(20) not null,
  constraint hire_jobs_pk primary key (id)
);

-- Curriculos
-- DROP: drop table hire_curriculums;
create table hire_curriculums (
  id bigserial not null,
  hire_candidate_id bigint not null,
  works_history text,
  academics_history text,
  courses text,
  summary text,
  constraint hire_curriculums_pk primary key (id)
);

alter table public.hire_curriculums add constraint hire_curriculums_fk_hire_candidates foreign key (hire_candidate_id) references public.hire_candidates(id);

-- Candidaturas
-- DROP: drop table hire_process;
create table hire_process (
  id bigserial not null,
  hire_candidate_id bigint not null,
  hire_job_id bigint not null,
  status varchar(20) not null,
  constraint hire_process_pk primary key (id),
  constraint hire_process_uk1 UNIQUE (hire_candidate_id, hire_job_id)
);

alter table public.hire_process add constraint hire_process_fk_hire_curriculums foreign key (hire_candidate_id) references public.hire_candidates(id);
alter table public.hire_process add constraint hire_process_fk_hire_jobs foreign key (hire_job_id) references public.hire_jobs(id);


insert into hire_candidates (name, age, city, phone, email, status) values ('Maria José', 23, 'Belo Horizonte', '(31) 93457-4588', 'mariajose@tempmail.com', 'Active');
insert into hire_candidates (name, age, city, phone, email, status) values ('João Batista', 24, 'Belo Horizonte', '(31) 99276-7839)', 'joaobatista@tempmail.com', 'Active');
insert into hire_candidates (name, age, city, phone, email, status) values ('Raimundo Nonato', 25, 'São Paulo', '(11) 95555-4444', 'raimundononato@tempmail.com', 'Canceled');
insert into hire_candidates (name, age, city, phone, email, status) values ('Paulo José', 29, 'São Paulo', '(11) 92134-5609', 'paulojose@tempmail.com', 'Active');

insert into hire_jobs (description, status) values ('Analista de Sistemas Fullstack', 'Active');
insert into hire_jobs (description, status) values ('Analista de Negócios', 'Process');
insert into hire_jobs (description, status) values ('Analista de Testes', 'Active');

insert into hire_curriculums (hire_candidate_id, works_history, academics_history, courses, summary) values (1,'','','','');
insert into hire_curriculums (hire_candidate_id, works_history, academics_history, courses, summary) values (2,'','','','');
insert into hire_curriculums (hire_candidate_id, works_history, academics_history, courses, summary) values (4,'','','','');

insert into hire_process (hire_candidate_id, hire_job_id, status) values (1,1,'Active');
insert into hire_process (hire_candidate_id, hire_job_id, status) values (1,2,'Active');
