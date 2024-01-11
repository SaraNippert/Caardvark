USE master
GO

--Database Drop and Create
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--Create Tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE decks (
	deck_id int IDENTITY(1,1) NOT NULL,
	title varchar(100) NOT NULL,
	tags varchar(50) NOT NULL,
	[desc] varchar(200) NOT NULL,
	user_id int NOT NULL,
	CONSTRAINT PK_deck PRIMARY KEY (deck_id)
)

CREATE TABLE cards (
	card_id int IDENTITY(1,1) NOT NULL,
	term varchar(200) NOT NULL,
	definition varchar(300) NOT NULL,
	user_id int NOT NULL
	CONSTRAINT PK_card PRIMARY KEY (card_id)
)

CREATE TABLE cardxdeck (
	card_id int NOT NULL,
	deck_id int NOT NULL
	CONSTRAINT PK_cardxdeck PRIMARY KEY (card_id, deck_id)
)


--Default Users
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');


--All Decks
INSERT INTO decks (title, tags, [desc], user_id) 
	VALUES 
	('Spanish Greetings', 'Spanish', 'The basics of greeting for beginners in Spanish.', 1),
	('Circulatory System', 'Anatomy', 'The basics of the curculatory system.', 2),
	('Astronomy Basics', 'Astronomy', 'Foundational theories and terms of astronomy', 2),
	('French Greetings', 'French', 'The basics of greeting for beginners in Spanish.', 1),
	('Basics of Nutrition', 'Nutrition', 'Fundamental terms and ideas for learning nutrition.', 1)


--All Available Cards
INSERT INTO cards (term, definition, user_id)
	VALUES 
	
	--Spanish Greetings
	('Hola', 'Hello', 1),
	('Buenas tardes', 'Good evening', 1),
	('Buenos días', 'Good morning', 1),
	('Buenas noches', 'Good night', 1),
	('Adiós', 'Goodbye (Farewell)', 1),
	('Hasta luego', 'See you later', 1),
	('Qué gusto de verlo', 'What a pleasure to see you', 1),
	('¿Qué pasa?', 'What''s up?', 1),
	('Cómo estás', 'How are you', 1),
	('Hasta mañana', 'See you tommorow', 1),
	
	--Circulatory System
	('capillaries', 'The site of exchange with tissues.', 1),
	('portal veins', 'Veins that extend some distance between two capillary networks.', 1),
	('arteriosclerosis', 'general term for degeneration changes in arteries making them less elastic.', 1),
	('atherosclerosis', 'Term for deposition of plaque on walls.', 1),
	('poiseulle`s Law', 'Flow decreases when resistance increases and vice versa.', 1),
	('baroreceptor reflex', 'Changing HR or stoke volume in response to change in BP.', 1),
	('chemoreceptor reflex', 'Sensory receptors detect oxygen, carbon dioxide and pH levels in the blood.', 1),
	('ischemic response', 'Results from high carbon dioxide or low pH, increases peripheral resistance.', 1),
	('net filtration', 'Force responsible for moving fluid across capillary walls.', 1),
	('artery', 'Muscular-walled tubes forming part of the circulation system by which oxygenated blood is conveyed.', 1),
	
	--Astronomy Basics
	('astronomy', 'The study of the moon.', 2),
	('asteroids', 'Objects that are revolving around the sun that are to small and too numerous to be considered planets.', 2),
	('black hole', 'The remains of an extremely massive star pulled into a small volume by the force of gravity.', 2),
	('comet', 'A ball of ice and dust whose orbit is a long, narrow ellipse.', 2),
	('galaxy', 'A giant structure that contains hundreds of billions of stars.', 2),
	('gas giants', 'The name given to the first four outer planets: Jupiter, Saturn, Uranus, and Neptune.', 2),
	('giant star', 'A very large star, much larger than the sun.', 2),
	('gravity', 'The attractive force between two objects; the amount of force depends on their masses and the distance between them.', 2),
	('light year', 'The distance that light travels in one year.', 2),
	('meteor', 'a streak of light in the sky produced by the burning of a meteoroid in Earth''s atmosphere.', 2),
	
	--French Greetings
	('Bonjour', 'Hello', 2),
	('Salut', 'Hi', 2),
	('Coucou', 'Hey', 2),
	('Bonsoir', 'Good evening', 2),
	('Au revoir', 'Bye', 2),
	('Pardon', 'Excuse me', 2),
	('À bientôt', 'See you soon', 2),
	('À demain', 'See you tomorrow', 2),
	('À plus tard', 'See you later', 2),
	('Bon weekend', 'Have a good weekend', 2),

	--Basics of Nutrition
	('nutrition', 'The science of food and the ways in which the body uses the foods you eat.', 2),
	('nutrients', 'Substances in the food that provide energy and help form body tissues. Necessary for growth and life.', 2),
	('carbohydrates', 'Class of energy giving nutrients that include starches, fibers and sugars.', 2),
	('glucose', 'Simple sugar that circulates in the blood. Provides energy to the body''s cells.', 2),
	('fructose', 'Sugar that is naturally found in fruit and honey.', 2),
	('lactose', 'Sugar made by animals.', 2),
	('sucrose', 'A sugar refined from sugar cane that is known as table sugar.', 2),
	('glycogen', 'Complex carbohydrate made in the body, stored in the liver and muscle of humans.', 2),
	('fiber', 'Adds bulk to your body''s waste.', 2),
	('fats', 'Class of energy giving nutrients that are also the main form of energy storage in the body', 2)


--All Available Cards
INSERT INTO cardxdeck (card_id, deck_id)
	VALUES
	(1, 1),
	(2, 1),
	(3, 1),
	(4, 1),
	(5, 1),
	(6, 1),
	(7, 1),
	(8, 1),
	(9, 1),
	(10, 1),

	(11, 2),
	(12, 2),
	(13, 2),
	(14, 2),
	(15, 2),
	(16, 2),
	(17, 2),
	(18, 2),
	(19, 2),
	(20, 2),

	(21, 3),
	(22, 3),
	(23, 3),
	(24, 3),
	(25, 3),
	(26, 3),
	(27, 3),
	(28, 3),
	(29, 3),
	(30, 3),

	(31, 4),
	(32, 4),
	(33, 4),
	(34, 4),
	(35, 4),
	(36, 4),
	(37, 4),
	(38, 4),
	(39, 4),
	(40, 4),

	(41, 5),
	(42, 5),
	(43, 5),
	(44, 5),
	(45, 5),
	(46, 5),
	(47, 5),
	(48, 5),
	(49, 5),
	(50, 5)

--Foreign Key Constraints

ALTER TABLE decks ADD CONSTRAINT user_id_FK_on_decks FOREIGN KEY (user_id) REFERENCES users(user_id);
ALTER TABLE cards ADD CONSTRAINT user_id_FK_on_cards FOREIGN KEY (user_id) REFERENCES users(user_id);
ALTER TABLE cardxdeck ADD CONSTRAINT deck_id_FK FOREIGN KEY (deck_id) REFERENCES decks(deck_id);
ALTER TABLE cardxdeck ADD CONSTRAINT card_id_FK FOREIGN KEY (card_id) REFERENCES cards(card_id);

GO