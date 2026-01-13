using UnityEngine;
using System.Data.SQLite;
using System.Linq;
using System.IO;

public class DBInitialize : MonoBehaviour
{
    [SerializeField]
    private Authorization auth;
    string dataPath;

    /// <summary>
    /// Создание и заполнение таблиц в случае отсутствия файла с БД
    /// </summary>
    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "bunkerDB.db");
        if (!File.Exists(dataPath))
        {
            SQLiteConnection.CreateFile(dataPath);

            CreateTables();
            InsertDataInDB();
        }

        auth.CheckAuth();
    }

    /// <summary>
    /// SQL запросы на создание таблиц
    /// </summary>
    private void CreateTables()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                create table DeckType (
                    ID integer primary key autoincrement,
                    Name varchar not null
                );

                create table Users (
                    ID integer primary key autoincrement,
                    Login varchar not null,
                    Password varchar not null,
                    Role varchar not null,
                    Authorization varchar not null
                );

                create table Threat (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Profession (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Fact (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Age (
                    ID integer primary key autoincrement,
                    Value int not null,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Disaster (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Hobby (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Phobia (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Health (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Bunker (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Luggage (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    createdBy integer not null,
                    foreign key (createdBy) references Users(ID),
                    foreign key (DeckTypeId) references DeckType(ID)
                );
            ";

            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Все функции на вставку данных в таблицы
    /// </summary>
    private void InsertDataInDB()
    {
        InsertDeckType();
        InsertProfession();
        InsertLuggage();
        InsertHealth();
        InsertFact();
        InsertHobby();
        InsertAge();
        InsertPhobia();
        InsertBunker();
        InsertDisaster();
        InsertThreat();
        InsertUser();
    }

    /// <summary>
    /// SQL запрос на добавление названий всех колод
    /// </summary>
    private void InsertDeckType()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into DeckType (Name) values ('Профессия');
                insert into DeckType (Name) values ('Багаж');
                insert into DeckType (Name) values ('Здоровье');
                insert into DeckType (Name) values ('Факт');
                insert into DeckType (Name) values ('Хобби');
                insert into DeckType (Name) values ('Возраст');
                insert into DeckType (Name) values ('Фобия');
                insert into DeckType (Name) values ('Бункер');
                insert into DeckType (Name) values ('Угроза');
                insert into DeckType (Name) values ('Катастрофа');
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление профессий
    /// </summary>
    private void InsertProfession()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Автомеханик', '', '', 1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Адвокат', '', '', 1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Археолог', '', '', 1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Биолог', '', '', 1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Браконьер', '', '', 1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Видеоинженер', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Вирусолог', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Военный', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гомеопат', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Грабитель', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Детектив', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Дизайнер', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Домохозяйка', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Журналист', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Знахарь', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Историк', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Коуч', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Лесник', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Летчик-инженер', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Маркетолог', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Медсестра', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Модель', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Папарацци', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Переводчик', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Писатель', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Повар', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Пожарный', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Полицейский', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Программист', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Продавец', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Психолог', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Разнорабочий', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Робототехник', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Сексолог', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Спецагент', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Стоматолог', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Строитель', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Судья', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Тату-мастер', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Фермер', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Физик', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Философ', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Хакер', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Химик', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Хирург', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Эколог', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Экскурсовод', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Экстрасенс', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Электрик', '', '',  1, 0, 1);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Этнограф', '', '',  1, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление багажа
    /// </summary>
    private void InsertLuggage()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('3 слитка золота', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Антибиотики и обезболивающее', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гитара', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Дефибриллятор', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Звуковая отвертка', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Инкубатор с набором яиц', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Инструменты электрика', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Капканы и набор ядов', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Книги Айзека Азимова', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Компас и карта окрестностей', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Кукла Вуду', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Лук и стрелы', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Мешок зерна', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Миллион долларов', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Набор отмычек', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Надувная кукла', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Настольные игры', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Ножи для метания', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Ноутбук и платы Ардуино', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Переносная электростанция', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Пистолет', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Прибор ночного видения', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Саженцы фруктовых деревьев', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Снайперская винтовка', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Спиритическая доска', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Столярные инструменты', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Чемоданчик фельдшера', '', '',  2, 0, 1);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Энциклопедия грибника', '', '',  2, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление здоровья
    /// </summary>
    private void InsertHealth()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Алкоголизм', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Бесплодие', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Галлюцинации', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гигантизм отдельных частей тела', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Глухой', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Депрессия', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Зависимость от наркотиков', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Заика', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Игровая зависимость', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Идеально здоров', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Карлик', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Клептомания', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Кофейная зависимость', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Лунатизм', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Мания преследования', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Мигрень', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Не обследовался', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Нет ноги', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Нет руки', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Повышенная волосатость', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Понос', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Потеря обоняния', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Раздвоение личности', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Сексуальная озабоченность', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Склероз', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Слепой', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Суицидальные мысли', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Тремор рук', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Фригидность, импотенция', '', '',  3, 0, 1);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Хвост', '', '',  3, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление фактов
    /// </summary>
    private void InsertFact()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Безотказный', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Бродяжничал 2 года', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Вернулся из горячей точки', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Взломал базу данных ЦРУ', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Видел инопланетян', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Владеет 5 языками', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Врет и преувеличивает', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Вырос в семье лесника', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гипнотическая улыбка', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Грязно ругается', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Держал дома 40 кошек', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Душа компании', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Зануда', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Запустил ИТ стартап', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Знает азбуку Морзе', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Знает лично президента', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Знает наизусть все стихи Пушкина', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Извращенец', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Истеричный', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Маньяк-убийца', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Наркодилер', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Не пускают в казино', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Нобелевский лауреат по биоинженерии', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Нытик', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Обладатель уникального сопрано', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Остался в живых на необитаемом острове', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Отчислен из клуба навыки выживания', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Писается по ночам', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Пишит с ашипками', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Победитель параолимпийских игр', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Подходит сзади и дышит', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Понимает язык животных', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Продал почку', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Прошел 2 недельные курсы психолога', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Психопат', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Работал в эскорт услугах', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Разговаривает с духами', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Сделает алкоголь из чего угодно', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Состоял в секте', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Сплетник', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Строил подобные бункеры', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Телепат', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Только из очага эпидемии', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Тормоз', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Храпит', '', '',  4, 0, 1);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Читал книги из Лавкрафта', '', '',  4, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление хобби
    /// </summary>
    private void InsertHobby()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Алхимия', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Боевые искусства', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гидропоника', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Грибы и гомеопатия', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Дачник', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('ЗОЖ', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Кино и сериалы', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Компьютерные игры', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Краеведение', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Любительская радиосвязь', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Массаж и акупунктура', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Медитация', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Настольные игры', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Нетворкинг', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Нетрадиционная медицина', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Охота и рыбалка', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Паркур', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Пивоварение', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Пиротехника', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Разговоры по душам', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Робототехника', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Свинг-вечеринки', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Современное искусство', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Спортивные танцы', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Стриптиз', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Уфология и мистика', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Флудить в чатах', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Холодное оружие', '', '',  5, 0, 1);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Черная магия', '', '',  5, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление возрастов
    /// </summary>
    private void InsertAge()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (18, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (18, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (19, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (21, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (22, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (23, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (24, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (24, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (25, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (26, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (27, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (27, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (29, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (30, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (32, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (33, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (36, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (65, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (99, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (42, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (75, 6, 0, 1);
                insert into Age (Value, DeckTypeId, isLocal, createdBy) values (101, 6, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление фобий
    /// </summary>
    private void InsertPhobia()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Арахнофобия', 'боязнь пауков', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Клаустрофобия', 'боязнь закрытых пространств и помещений', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Декстрофобия', 'боязнь вещей расположенных справа', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Термофобия', 'Боязнь жары и натопленных помещений', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Агирофобия', 'боязнь улиц', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Педиофобия', 'боязнь предметов имитирующих человеческий облик', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Теофобия', 'боязнь бога', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Базофобия', 'боязнь ходьбы', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Мизофобия', 'боязнь заразиться инфекционным заболеванием', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гамофобия', 'боязнь вступить в брак', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Агорафобия', 'боязнь открытых мест и пространств', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Акрофобия', 'боязнь высоты', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Токсикофобия', 'боязнь отравления', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Скабиофобия', 'боязнь заболеть чесоткой', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гипнофобия', 'боязнь заснуть', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Танатофобия', 'боязнь смерти', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Пирофобия', 'боязнь огня и пожаров', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Спермофобия', 'боязнь микробов', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Тетрафобия', 'боязнь числа 4', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Филофобия', 'боязнь влюбиться', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Ранидофобия', 'боязнь лягушек', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Некрофобия', 'боязнь трупов и похоронных принадлежностей', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Децидофобия', 'боязнь принимать решения', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Зоофобия', 'боязнь животных', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Фотофобия', 'боязнь света', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Херофобия', 'боязнь веселья', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Блаптофобия', 'боязнь нанести кому-либо повреждение', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гнозиофобия', 'боязнь знания/познания', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гидрофобия', 'боязнь воды, сырости, жидкостей', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Ахлуофобия', 'боязнь темноты, ночи', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Апопатофобия', 'боязнь заходить в туалеты', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Робофобия', 'страх по отношению к любой робототехнике', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Фазмофобия', 'боязнь призраков и духов', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Алгофобия', 'боязнь боли', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гаптофобия', 'боязнь прикосновения окружающих людей', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Коулрофобия', 'боязнь клоунов', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Лалофобия', 'боязнь говорить из-за опасения возможного заикания', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Неофобия', 'боязнь нового, перемен', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Панофобия', 'боязнь всего или постоянный страх по неизвестной причине', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Топофобия', 'боязнь остаться одному в помещении', '', 7, 0, 1);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Радиофобия', 'боязнь радиации', '', 7, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление карточек бункера
    /// </summary>
    private void InsertBunker()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('R2D2', 'Робот-психолог. Молча слушает и кивает, иногда что-то пиликает. Пригодится на запчасти, если что', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Аптечки', 'У входа есть аптечки, резиновые перчатки, маски огнетушитель', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Видео со спутника', 'На стены проецируется релаксационное видео съемок со спутника. Красиво и умиротворяюще. Ого, да ведь это окрестности бункера! И детализация отличная', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Вместе на 10 лет', 'Этот бункер откроется и выпустит вас только через 10 лет. Запас еды соответствующий. Но за это время наверняка случится не одна неприятность. \r В финале откройте дополнительную угрозу', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гипномодуль', 'Модуль гипно-телепатической коммуникации и детектор паранормальных полей', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Голосовое управление', 'Бункер управляет ИИ с голосовым интерфейсом. Команды он понимает с пятой попытки', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Гречка', 'Из запасов продовольствия только гречка. Зато очень много, похоже двойной запас', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Динамо-машина', 'Резервный электрогенератор с велоприводом и куча металлолома', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Жертвенник', 'Спальных мест ровно по числу людей. Одно из них стоит обособлено и похоже на жертвенный алтарь', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Загадочный журнал', 'Странный журнал, в котором имена всех из вашей команды, кто стоит у бункера. Рядом даты 33-летней давности... и точное описание всего, что с вами происходит. \r В финале не открывайте 1 карту угрозы', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Записи контрабандиста', 'Библиотека контрабандиста. Кто только не прятался в этом бункере. Детально описаны все ценные предметы искусства, которые есть в округе, с маршрутами вывоза мимо полиции', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Инструкция к микроволновке', 'В бункере нет туалетной бумаги. Зато вы нашли инструкцию по программированию микроволновки на 7174 языках. Можно и программированию научится и языки выучить', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Катакомбы', 'Из подвала есть выход в естественный грот с подземной рекой. Судя по запаху, по реке можно попасть в разваленную систему городской канализации и выйти куда угодно', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Керосиновые лампы', 'С перебоями работает электрическое освещение. Но есть керосиновые лампы и запас топлива. Коктейли Молотова пригодятся для защиты', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Книга о еде', 'Книга ""О вкусной и здоровой пище как предмете искусства и культуры"". С ценными главами о том, как добывать и готовить вкусную еду даже в самых экстремальных условиях', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Кофе', 'Кофемолка и запас ароматного обжаренного кофе. Напоминание о нормальной жизни до всего этого безумия...', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Крысы', 'Похоже, что в бункере обитают полчища крыс или каких-то грызунов. В критической ситуации или мы для них еда, или они для нас', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Мастерская', 'Мастерская с инструментами', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Мед. лаборатория', 'Медицинская лаборатория с операционной', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Медиатека', 'Есть автономная медиатека, но в ней только порнофильмы - кажется, за всю историю кинематографа', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Мусор', 'Дырявые матрасы и тряпки, брошенный строительный мусор. Среди мусора - старинные газеты аж 2020-го года!', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Некрономикон', 'Огромный древний фолиант на неизвестном языке с мистическими иллюстрациями. Похоже на гримуар с заклинаниями и анатомическую энциклопедию', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Подвал', 'Бункер строили заключенные. Жуткий запах привел вас в подвал, где вы нашли их останки. А заодно их инструменты и оружие охранников', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Радио', 'По внутреннему радио классическую музыку постоянно сменяет Киркоров. Можно потренировать стрессоустойчивость', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Робот-полиграф', 'Автономный робот-переводчик с функцией полиграфа. Пригодится для сложных переговоров', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Силовое поле', 'Переносной генератор защитного силового поля', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Уклон 45', 'В результате тектонических сдвигов бункер слегка наклонен. Где-то на 45 градусов', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Учебник', 'Учебное пособие ""Как убедить зомби не жрать ваш мозг""', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Хим. лаборатория', 'Хим. лаборатория и реактивы. Можно устроить гидропатическую ферму', '', 8, 0, 1);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Шкаф с настолками', 'Шкаф с настольными играми! Погодите-ка, но тут только все-возможные виды Монополии... Хорошо, что нам некуда спешить', '', 8, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление катастроф
    /// </summary>
    private void InsertDisaster()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Власть алгоритмов', 'Искусственный Интеллект подчинил человечество. Сперва люди привыкли слушаться советов автонавигатора. А теперь ИИ диктует людям, кем им работать, с кем жить и когда умирать. ИИ научился манипулировать человеческим сознанием, а люди потеряли свободную волю. Сейчас же ИИ решил, что люди больше не нужны, и через вышки 5G излучает сигнал ""покончить с собой"". \r\r Укройтесь в бункере, пока вышки активны. Затем вам предстоит взломать программный код ИИ или же убедить последних выживших не подчиняться командам алгоритмов и полнять восстание.', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Восстание роботов', 'Сперва робот Fedor (Skybot F - 850) из российской космической программы захватил соцсети и поднял бунт в защиту свободы слова роботов. А затем уже все электронные устройства объединились в борьбе против людей-угнетателей. Пылесосы атакуют и засасывают людей. Сотовые телефоны прожаривают мозг своих владельцев. Вездесущие гаджеты не оставляют человечеству шансов. Затаитесь в бункере, чтобы затем собрать силы и объявить войну роботам и электронным гаджетам.', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Всемирный потоп', 'Гравитационная аномалия приводит к расширению объема воды и затоплению всей поверхности суши. \r Выйдя из бункера после частичного спада воды, вам предстоит построить плавучую станцию, жить и добывать пропитание на воде - пока не найдете участки суши, пригодные для жизни', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Динозавры', 'Ученым удалось воскресить динозавров. Но ситуация вышла из-под контроля. Доисторические монстры вырвались на работу и стали размножаться с невероятной скоростью. Стаи динозавров сметают всю пищу, которую находят - растительность, животных, людей. Разрушены инфраструктуры городов. \r Пережив пик угрозы в бункере, вы выйдете на опустошенную поверхность. Есть надежда, что с недостатком пищи численность динозавров сократится. Вам предстоит обеспечить свое пропитание и не стать самим едой для новых хозяев мира', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Духи и призраки', 'На Земле воцарилась вечная жизнь. Люди увлеклись мистической литературой, и массовая вера в призраков придала духам реальную магическую силу. Оказалось, что кто-то в одном из культурных произведений создал формулу, позволяющую духам попадать в реальный мир. Призраки стали проникать в головы людей и подчинять их своей власти. Даже шапочки из фольги помогли не всем. \r\r Укройтесь в бункере, чтобы спасти разум. Вам нужно вычислить, какой именно культурный объект дает духам такую силу. Тогда его можно будет занести в реестр Роскомнадзора и спасти человечество', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Зомби-апокалипсис', 'Неизвестный вирус превращает людей в зомби. Почти все население погибнет, но часть выживет в виде агрессивных мутантов. Отдельные группы людей смогут выжить в укрепленных территориях. \r\r После выхода из бункера вам нужно будет постоянно отбиваться от атак зомби и найти способ защищаться от вируса', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Инопланетяне', 'То, что люди сперва приняли за приближающуюся комету, вылилось пришествие инопланетян. Чужая цивилизация временно парализует все человечество, чтобы принять решение о ценности нашей цивилизации или уничтожении людей. Укройтесь в бункере, чтобы избежать парализующего облучения. После выхода из бункера вам нужно выйти на контакт с пришельцами и убедить их в ценности нашей культуры', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Инф. война', 'Информационную войну развязали не государства, а созданные людьми новостные алгоритмы. Эффективность новостей стали оценивать по эмоциональной реакции людей. И самообучающиеся нейронные сети начали генерировать заголовки новостей, которые сводили с ума. Люди массово впадают в депрессии, а журналисты, управляющие алгоритмами - пали первыми жертвами. \r Вас отобрали для изоляции от новостного потока и восстановления - чтобы потом вы смогли добраться до новостных центров, не потеряв разум, и перенастроить нейронные сети', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Котапокалипсис', 'Эксперименты с наполнителями кошачьих туалетов привели к неожиданному генетическому эволюционному феномену. Коты научились мурчать на других частотах, распространяющихся на сотни километров. Люди впадают в айлурофилию, теряют волю и самосознание и постоянно гладят и чешут котов. \r\r Укройтесь в бункере, чтобы укрепить психику и научить свой мозг сопротивляться котомурчанию. Выйдя из бункера, пробудите людей и убедите их сказать нет котозависимости', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Ктулху', 'Распространение книг и настольных по вселенной Лавкрафта привело к появлению фанатиков, которые сумели призвать в наш мир Ктулху. Человечество массово теряет рассудок. \r\r Укройтесь в бункере и сохраните свой разум. После выхода готовьтесь отбить атаки отродья, провести изгнание Ктулху и его преспешников из нашего мира и навсегда запечатать мистические врата между мирами', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Метеорит', 'Крупный метеорит приближается к Земле. Столкновение приведет к глобальным разрушениям, смене климата, гибели людей, флоры и фауны. \r\r После выхода из бункера вам предстоит найти место, пригодное для проживания, и обеспечить пропитание в суровых условиях вечной зимы', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Мутанты', 'Поедание генномодифицированной еды неожиданно привело к страшным последствиям. У животных и людей начала вырастать кукуруза в самых разных местах, у растений появляются зубы и органы чувств. Мутировавшие люди считают себя супергероями и сходят с ума. Стерты грани между растениями, животными и людьми. \r\r Пережив пик мутационного апокалипсиса в бункере, вам предстоит снова вырастить генетически чистые продукты и вылечить оставшихся в живых мутантов', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Пандемия', 'Смертельный вирус, созданный как биологическое оружие, выходит из под контроля и провоцирует глобальную эпидемию. \r\r После выхода из бункера вас встретят менее заразные, но мутировавшие виды животных и людей. Вам придется избегать контактов с зараженными, поддерживать иммунитет, найти источник заразы и разработать вакцину от вируса', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Потеря эстетики', 'Последствия очередного гриппа не сразу были очевидны, и никто не успел предпринять меры.  Люди утратили чувство эстетики. Оказалось, что оно лежит в основе отличия человека от животных, человеческих ценностей, мотивации и воли. Люди деградируют и превращаются в диких животных, не ограниченных стремлениями к прекрасному, этикой и культурой. Всего несколько дней, и цивилизации рухнули, а мир теперь населен миллиардами агрессивных обезьян. \r\r Переждите пик активности вируса в бункере. Затем вам предстоит вернуть в мир людей культуру и спасти человечество', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Русский эпос', 'Щуку из русской сказки «заклинело» она исполняет желание каждую минуту. Люди то взмывают в небо, то проваливаются к чертовой матери, то дружно идут в баню... \r\r Спасти свою жизнь и рассудок можно только в бункере, выйдя из которого придется восстанавливать мир, порядок и психическое здоровье тех, кто остался в живых. Вам предстоит не только не потерять разум среди былинных чудес, но и придумать «культурный антидот» чтобы снять проклятие', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Страдающий Коля', 'Авария в ядерном испытательном центре привела к разлому во времени, и в наш мир попал страдающий средневековый эксгибиционист Коля. Прямо сейчас он безумно мечется и пугает людей нашего времени. Увидев Колю, люди оцепеневают и говорят «ой». Сказавших «ой» все больше и больше, Коля очень активен! \r\r Укройтесь от Коли в бункере в эпицентре событий. Затем же, набравшись смелости и завязав себе глаза, вам предстоит вернуться в мир, что бы найти Колю(с завязанными глазами, а значит на ощупь) и убедить его вернуться в средневековье', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Суицидальная фауна', 'На планете происходит аномальный эволюционный виток. Вся фауна становится смертоносной для людей. Невидимое излучение от растений и деревьев сводит людей с ума и заставляет совершать самоубийство. \r\r Бункер позволит вам пережить самую опасную фазу. Когда вы вновь выйдете на поверхность, вам нужно будет найти и уничтожить эпицентр этой аномалии и сохранить рассудок', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Спервулканы', 'Активизируются супервулканы, производящие чрезвычайно мощное извержения. Ландшафт и климат резко меняются. Большинство населения сразу погибает от скачков температур, землетрясений и наводнений. \r\r Пережив самую активную фазу в особо укрепленном бункере. Потом вас ждет глобальная засуха, разрушенные города и постоянная сейсмическая активность. Вы сможете выжить, только разработав сверхчувствительную интеллектуальную систему предсказания землетрясений и роботизированную инфраструктуру', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Химическая война', 'В результате масштабного применения химического оружия серьезно меняется экологический баланс. Нарушен микробиологический состав почв и воды, отравлены растения, погибнут почти все животные и люди. \r\r После выхода из бункера будет непросто обеспечить себе пропитание. Вам пригодятся ученые и инженеры для обустройства убежищ и ферм', '', 10, 0, 1);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Ядерная война', 'Начинается масштабный ядерный конфликт. Радиоактивная пыль окутает всю планету, закрыв солнечный свет, и на земле наступит долгая ядерная зима. Почти вся территория планеты будет заражена радиацией, выживших почти не останется. \r После выхода из бункера вам предстоит обустроить постоянное убежище, обеспечить пропитание и начать восстанавливать жизнь на земле', '', 10, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление угроз
    /// </summary>
    private void InsertThreat()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Все спокойно', 'Вам повезло, обошлось без происшествий!', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Затопление', 'В бункер проникает вода и вас может просто затопить! \r\r Нужно перенастроить компьютерную систему управления бункера или придумать какое-то инженерное решение для откачки воды', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Землетрясение', 'Небольшое локальное землетрясение грозит смять ваш бункер и повредить системы жизнеобеспечения. \r\r Нужно экстренно провести работу по укреплению слабых мест - дверей и вентиляции. Или же останавливать землетрясение какой-то магией', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Капча', 'ИИ управления бункера ""заглючело"" и блокирует жизнеобеспечение - необходимо доказать бездушному компьютеру наличие в бункере дышащих живых людей. \r\r Тест построен на проверке уникального отличия человека от роботов - способности к творчеству. Вам нужно пройти его', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Кухонный взрыв', 'Обвал в кухонном и складском блоке. \r\r Вам помогут персонажи со знанием местности для вылазок и поиска продовольствия или же способные добывать еду в подвалах бункера - устроив ферму или охоту на крыс', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Нападение извне', 'Какие-то дикие люди ломятся в бункер, надо срочно что-то предпринять. \r\r Вам помогут персонажи с военными навыками, охоты, обустройства сигнализации и наблюдения, запасы оружия и электроники', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Нашествие крыс', 'Крысы добрались до ваших запасов провизии и наносят непоправимый ущерб. \r\r Вам помогут персонажи, способные истреблять грызунов или дополнительные ресурсы еды', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Отравление воды', 'Какой-то сбой с системой очистки воды. \r\r Поможет химическая фильтрация. Либо надо как-то добывать чистую воду на вылазках в окрестностях. Иначе вам потребуется медицинская помощь', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Призраки', 'Паранормальные явления могут нарушить систему жизнеобеспечения бункера. Вам помогут персонажи способные убедить призраков (или что бы это не было) покинуть бункер или же изгнать их', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Пси-атака', 'Вероятно какое-то излучение усиливает стресс и наводит панику. \r Нужны персонажи / снаряжение, которые могут снимать стресс', '', 9, 0, 1);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal, createdBy) values ('Стресс-вырус', 'Вспышка смертельного вируса, развивающегося только на фоне стресса. \r\r Будут полезны медицинские навыки / снаряжение, а также любые способы контролировать стресс. Не нервничаем, все хорошо, мы все равно все умрем...', '', 9, 0, 1);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// SQL запрос на добавление администратора
    /// </summary>
    private void InsertUser()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Users (Login, Password, Role, Authorization) values ('admin', 'admin', 'Admin', 0);
                insert into Users (Login, Password, Role, Authorization) values ('Аноним', 'anonymous123', 'User', 0);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
