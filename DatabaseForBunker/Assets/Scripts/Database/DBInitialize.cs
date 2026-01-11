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

        auth.Start();
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
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Profession (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Fact (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Age (
                    ID integer primary key autoincrement,
                    Value int not null,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Disaster (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Hobby (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Phobia (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Health (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Bunker (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
                    foreign key (DeckTypeId) references DeckType(ID)
                );

                create table Luggage (
                    ID integer primary key autoincrement,
                    Name varchar not null,
                    Description varchar,
                    ModelPath varchar,
                    DeckTypeId varchar not null,
                    isLocal varchar not null,
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
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Автомеханик', '', '', 1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Адвокат', '', '', 1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Археолог', '', '', 1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Биолог', '', '', 1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Браконьер', '', '', 1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Видеоинженер', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Вирусолог', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Военный', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гомеопат', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Грабитель', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Детектив', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Дизайнер', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Домохозяйка', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Журналист', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Знахарь', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Историк', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Коуч', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Лесник', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Летчик-инженер', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Маркетолог', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Медсестра', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Модель', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Папарацци', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Переводчик', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Писатель', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Повар', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Пожарный', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Полицейский', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Программист', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Продавец', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Психолог', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Разнорабочий', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Робототехник', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Сексолог', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Спецагент', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Стоматолог', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Строитель', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Судья', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Тату-мастер', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Фермер', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Физик', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Философ', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Хакер', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Химик', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Хирург', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Эколог', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Экскурсовод', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Экстрасенс', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Электрик', '', '',  1, 0);
                insert into Profession (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Этнограф', '', '',  1, 0);
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
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('3 слитка золота', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Антибиотики и обезболивающее', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гитара', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Дефибриллятор', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Звуковая отвертка', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Инкубатор с набором яиц', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Инструменты электрика', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Капканы и набор ядов', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Книги Айзека Азимова', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Компас и карта окрестностей', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Кукла Вуду', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Лук и стрелы', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Мешок зерна', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Миллион долларов', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Набор отмычек', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Надувная кукла', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Настольные игры', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Ножи для метания', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Ноутбук и платы Ардуино', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Переносная электростанция', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Пистолет', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Прибор ночного видения', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Саженцы фруктовых деревьев', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Снайперская винтовка', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Спиритическая доска', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Столярные инструменты', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Чемоданчик фельдшера', '', '',  2, 0);
                insert into Luggage (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Энциклопедия грибника', '', '',  2, 0);
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
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Алкоголизм', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Бесплодие', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Галлюцинации', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гигантизм отдельных частей тела', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Глухой', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Депрессия', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Зависимость от наркотиков', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Заика', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Игровая зависимость', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Идеально здоров', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Карлик', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Клептомания', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Кофейная зависимость', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Лунатизм', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Мания преследования', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Мигрень', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Не обследовался', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Нет ноги', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Нет руки', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Повышенная волосатость', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Понос', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Потеря обоняния', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Раздвоение личности', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Сексуальная озабоченность', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Склероз', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Слепой', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Суицидальные мысли', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Тремор рук', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Фригидность, импотенция', '', '',  3, 0);
                insert into Health (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Хвост', '', '',  3, 0);
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
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Безотказный', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Бродяжничал 2 года', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Вернулся из горячей точки', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Взломал базу данных ЦРУ', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Видел инопланетян', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Владеет 5 языками', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Врет и преувеличивает', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Вырос в семье лесника', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гипнотическая улыбка', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Грязно ругается', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Держал дома 40 кошек', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Душа компании', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Зануда', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Запустил ИТ стартап', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Знает азбуку Морзе', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Знает лично президента', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Знает наизусть все стихи Пушкина', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Извращенец', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Истеричный', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Маньяк-убийца', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Наркодилер', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Не пускают в казино', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Нобелевский лауреат по биоинженерии', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Нытик', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Обладатель уникального сопрано', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Остался в живых на необитаемом острове', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Отчислен из клуба навыки выживания', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Писается по ночам', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Пишит с ашипками', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Победитель параолимпийских игр', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Подходит сзади и дышит', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Понимает язык животных', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Продал почку', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Прошел 2 недельные курсы психолога', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Психопат', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Работал в эскорт услугах', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Разговаривает с духами', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Сделает алкоголь из чего угодно', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Состоял в секте', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Сплетник', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Строил подобные бункеры', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Телепат', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Только из очага эпидемии', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Тормоз', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Храпит', '', '',  4, 0);
                insert into Fact (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Читал книги из Лавкрафта', '', '',  4, 0);
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
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Алхимия', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Боевые искусства', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гидропоника', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Грибы и гомеопатия', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Дачник', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('ЗОЖ', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Кино и сериалы', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Компьютерные игры', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Краеведение', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Любительская радиосвязь', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Массаж и акупунктура', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Медитация', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Настольные игры', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Нетворкинг', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Нетрадиционная медицина', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Охота и рыбалка', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Паркур', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Пивоварение', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Пиротехника', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Разговоры по душам', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Робототехника', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Свинг-вечеринки', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Современное искусство', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Спортивные танцы', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Стриптиз', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Уфология и мистика', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Флудить в чатах', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Холодное оружие', '', '',  5, 0);
                insert into Hobby (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Черная магия', '', '',  5, 0);
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
                insert into Age (Value, DeckTypeId, isLocal) values (18, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (18, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (19, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (21, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (22, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (23, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (24, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (24, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (25, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (26, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (27, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (27, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (29, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (30, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (32, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (33, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (36, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (65, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (99, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (42, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (75, 6, 0);
                insert into Age (Value, DeckTypeId, isLocal) values (101, 6, 0);
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
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Арахнофобия', 'боязнь пауков', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Клаустрофобия', 'боязнь закрытых пространств и помещений', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Декстрофобия', 'боязнь вещей расположенных справа', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Термофобия', 'Боязнь жары и натопленных помещений', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Агирофобия', 'боязнь улиц', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Педиофобия', 'боязнь предметов имитирующих человеческий облик', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Теофобия', 'боязнь бога', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Базофобия', 'боязнь ходьбы', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Мизофобия', 'боязнь заразиться инфекционным заболеванием', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гамофобия', 'боязнь вступить в брак', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Агорафобия', 'боязнь открытых мест и пространств', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Акрофобия', 'боязнь высоты', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Токсикофобия', 'боязнь отравления', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Скабиофобия', 'боязнь заболеть чесоткой', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гипнофобия', 'боязнь заснуть', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Танатофобия', 'боязнь смерти', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Пирофобия', 'боязнь огня и пожаров', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Спермофобия', 'боязнь микробов', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Тетрафобия', 'боязнь числа 4', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Филофобия', 'боязнь влюбиться', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Ранидофобия', 'боязнь лягушек', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Некрофобия', 'боязнь трупов и похоронных принадлежностей', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Децидофобия', 'боязнь принимать решения', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Зоофобия', 'боязнь животных', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Фотофобия', 'боязнь света', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Херофобия', 'боязнь веселья', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Блаптофобия', 'боязнь нанести кому-либо повреждение', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гнозиофобия', 'боязнь знания/познания', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гидрофобия', 'боязнь воды, сырости, жидкостей', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Ахлуофобия', 'боязнь темноты, ночи', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Апопатофобия', 'боязнь заходить в туалеты', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Робофобия', 'страх по отношению к любой робототехнике', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Фазмофобия', 'боязнь призраков и духов', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Алгофобия', 'боязнь боли', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гаптофобия', 'боязнь прикосновения окружающих людей', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Коулрофобия', 'боязнь клоунов', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Лалофобия', 'боязнь говорить из-за опасения возможного заикания', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Неофобия', 'боязнь нового, перемен', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Панофобия', 'боязнь всего или постоянный страх по неизвестной причине', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Топофобия', 'боязнь остаться одному в помещении', '', 7, 0);
                insert into Phobia (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Радиофобия', 'боязнь радиации', '', 7, 0);
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
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('R2D2', 'Робот-психолог. Молча слушает и кивает, иногда что-то пиликает. Пригодится на запчасти, если что', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Аптечки', 'У входа есть аптечки, резиновые перчатки, маски огнетушитель', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Видео со спутника', 'На стены проецируется релаксационное видео съемок со спутника. Красиво и умиротворяюще. Ого, да ведь это окрестности бункера! И детализация отличная', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Вместе на 10 лет', 'Этот бункер откроется и выпустит вас только через 10 лет. Запас еды соответствующий. Но за это время наверняка случится не одна неприятность. \r В финале откройте дополнительную угрозу', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гипномодуль', 'Модуль гипно-телепатической коммуникации и детектор паранормальных полей', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Голосовое управление', 'Бункер управляет ИИ с голосовым интерфейсом. Команды он понимает с пятой попытки', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Гречка', 'Из запасов продовольствия только гречка. Зато очень много, похоже двойной запас', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Динамо-машина', 'Резервный электрогенератор с велоприводом и куча металлолома', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Жертвенник', 'Спальных мест ровно по числу людей. Одно из них стоит обособлено и похоже на жертвенный алтарь', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Загадочный журнал', 'Странный журнал, в котором имена всех из вашей команды, кто стоит у бункера. Рядом даты 33-летней давности... и точное описание всего, что с вами происходит. \r В финале не открывайте 1 карту угрозы', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Записи контрабандиста', 'Библиотека контрабандиста. Кто только не прятался в этом бункере. Детально описаны все ценные предметы искусства, которые есть в округе, с маршрутами вывоза мимо полиции', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Инструкция к микроволновке', 'В бункере нет туалетной бумаги. Зато вы нашли инструкцию по программированию микроволновки на 7174 языках. Можно и программированию научится и языки выучить', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Катакомбы', 'Из подвала есть выход в естественный грот с подземной рекой. Судя по запаху, по реке можно попасть в разваленную систему городской канализации и выйти куда угодно', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Керосиновые лампы', 'С перебоями работает электрическое освещение. Но есть керосиновые лампы и запас топлива. Коктейли Молотова пригодятся для защиты', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Книга о еде', 'Книга ""О вкусной и здоровой пище как предмете искусства и культуры"". С ценными главами о том, как добывать и готовить вкусную еду даже в самых экстремальных условиях', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Кофе', 'Кофемолка и запас ароматного обжаренного кофе. Напоминание о нормальной жизни до всего этого безумия...', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Крысы', 'Похоже, что в бункере обитают полчища крыс или каких-то грызунов. В критической ситуации или мы для них еда, или они для нас', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Мастерская', 'Мастерская с инструментами', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Мед. лаборатория', 'Медицинская лаборатория с операционной', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Медиатека', 'Есть автономная медиатека, но в ней только порнофильмы - кажется, за всю историю кинематографа', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Мусор', 'Дырявые матрасы и тряпки, брошенный строительный мусор. Среди мусора - старинные газеты аж 2020-го года!', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Некрономикон', 'Огромный древний фолиант на неизвестном языке с мистическими иллюстрациями. Похоже на гримуар с заклинаниями и анатомическую энциклопедию', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Подвал', 'Бункер строили заключенные. Жуткий запах привел вас в подвал, где вы нашли их останки. А заодно их инструменты и оружие охранников', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Радио', 'По внутреннему радио классическую музыку постоянно сменяет Киркоров. Можно потренировать стрессоустойчивость', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Робот-полиграф', 'Автономный робот-переводчик с функцией полиграфа. Пригодится для сложных переговоров', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Силовое поле', 'Переносной генератор защитного силового поля', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Уклон 45', 'В результате тектонических сдвигов бункер слегка наклонен. Где-то на 45 градусов', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Учебник', 'Учебное пособие ""Как убедить зомби не жрать ваш мозг""', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Хим. лаборатория', 'Хим. лаборатория и реактивы. Можно устроить гидропатическую ферму', '', 8, 0);
                insert into Bunker (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Шкаф с настолками', 'Шкаф с настольными играми! Погодите-ка, но тут только все-возможные виды Монополии... Хорошо, что нам некуда спешить', '', 8, 0);
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
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Власть алгоритмов', 'Искусственный Интеллект подчинил человечество. Сперва люди привыкли слушаться советов автонавигатора. А теперь ИИ диктует людям, кем им работать, с кем жить и когда умирать. ИИ научился манипулировать человеческим сознанием, а люди потеряли свободную волю. Сейчас же ИИ решил, что люди больше не нужны, и через вышки 5G излучает сигнал ""покончить с собой"". \r\r Укройтесь в бункере, пока вышки активны. Затем вам предстоит взломать программный код ИИ или же убедить последних выживших не подчиняться командам алгоритмов и полнять восстание.', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Восстание роботов', 'Сперва робот Fedor (Skybot F - 850) из российской космической программы захватил соцсети и поднял бунт в защиту свободы слова роботов. А затем уже все электронные устройства объединились в борьбе против людей-угнетателей. Пылесосы атакуют и засасывают людей. Сотовые телефоны прожаривают мозг своих владельцев. Вездесущие гаджеты не оставляют человечеству шансов. Затаитесь в бункере, чтобы затем собрать силы и объявить войну роботам и электронным гаджетам.', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Всемирный потоп', 'Гравитационная аномалия приводит к расширению объема воды и затоплению всей поверхности суши. \r Выйдя из бункера после частичного спада воды, вам предстоит построить плавучую станцию, жить и добывать пропитание на воде - пока не найдете участки суши, пригодные для жизни', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Динозавры', 'Ученым удалось воскресить динозавров. Но ситуация вышла из-под контроля. Доисторические монстры вырвались на работу и стали размножаться с невероятной скоростью. Стаи динозавров сметают всю пищу, которую находят - растительность, животных, людей. Разрушены инфраструктуры городов. \r Пережив пик угрозы в бункере, вы выйдете на опустошенную поверхность. Есть надежда, что с недостатком пищи численность динозавров сократится. Вам предстоит обеспечить свое пропитание и не стать самим едой для новых хозяев мира', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Духи и призраки', 'На Земле воцарилась вечная жизнь. Люди увлеклись мистической литературой, и массовая вера в призраков придала духам реальную магическую силу. Оказалось, что кто-то в одном из культурных произведений создал формулу, позволяющую духам попадать в реальный мир. Призраки стали проникать в головы людей и подчинять их своей власти. Даже шапочки из фольги помогли не всем. \r\r Укройтесь в бункере, чтобы спасти разум. Вам нужно вычислить, какой именно культурный объект дает духам такую силу. Тогда его можно будет занести в реестр Роскомнадзора и спасти человечество', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Зомби-апокалипсис', 'Неизвестный вирус превращает людей в зомби. Почти все население погибнет, но часть выживет в виде агрессивных мутантов. Отдельные группы людей смогут выжить в укрепленных территориях. \r\r После выхода из бункера вам нужно будет постоянно отбиваться от атак зомби и найти способ защищаться от вируса', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Инопланетяне', 'То, что люди сперва приняли за приближающуюся комету, вылилось пришествие инопланетян. Чужая цивилизация временно парализует все человечество, чтобы принять решение о ценности нашей цивилизации или уничтожении людей. Укройтесь в бункере, чтобы избежать парализующего облучения. После выхода из бункера вам нужно выйти на контакт с пришельцами и убедить их в ценности нашей культуры', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Инф. война', 'Информационную войну развязали не государства, а созданные людьми новостные алгоритмы. Эффективность новостей стали оценивать по эмоциональной реакции людей. И самообучающиеся нейронные сети начали генерировать заголовки новостей, которые сводили с ума. Люди массово впадают в депрессии, а журналисты, управляющие алгоритмами - пали первыми жертвами. \r Вас отобрали для изоляции от новостного потока и восстановления - чтобы потом вы смогли добраться до новостных центров, не потеряв разум, и перенастроить нейронные сети', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Котапокалипсис', 'Эксперименты с наполнителями кошачьих туалетов привели к неожиданному генетическому эволюционному феномену. Коты научились мурчать на других частотах, распространяющихся на сотни километров. Люди впадают в айлурофилию, теряют волю и самосознание и постоянно гладят и чешут котов. \r\r Укройтесь в бункере, чтобы укрепить психику и научить свой мозг сопротивляться котомурчанию. Выйдя из бункера, пробудите людей и убедите их сказать нет котозависимости', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Ктулху', 'Распространение книг и настольных по вселенной Лавкрафта привело к появлению фанатиков, которые сумели призвать в наш мир Ктулху. Человечество массово теряет рассудок. \r\r Укройтесь в бункере и сохраните свой разум. После выхода готовьтесь отбить атаки отродья, провести изгнание Ктулху и его преспешников из нашего мира и навсегда запечатать мистические врата между мирами', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Метеорит', 'Крупный метеорит приближается к Земле. Столкновение приведет к глобальным разрушениям, смене климата, гибели людей, флоры и фауны. \r\r После выхода из бункера вам предстоит найти место, пригодное для проживания, и обеспечить пропитание в суровых условиях вечной зимы', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Мутанты', 'Поедание генномодифицированной еды неожиданно привело к страшным последствиям. У животных и людей начала вырастать кукуруза в самых разных местах, у растений появляются зубы и органы чувств. Мутировавшие люди считают себя супергероями и сходят с ума. Стерты грани между растениями, животными и людьми. \r\r Пережив пик мутационного апокалипсиса в бункере, вам предстоит снова вырастить генетически чистые продукты и вылечить оставшихся в живых мутантов', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Пандемия', 'Смертельный вирус, созданный как биологическое оружие, выходит из под контроля и провоцирует глобальную эпидемию. \r\r После выхода из бункера вас встретят менее заразные, но мутировавшие виды животных и людей. Вам придется избегать контактов с зараженными, поддерживать иммунитет, найти источник заразы и разработать вакцину от вируса', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Потеря эстетики', 'Последствия очередного гриппа не сразу были очевидны, и никто не успел предпринять меры.  Люди утратили чувство эстетики. Оказалось, что оно лежит в основе отличия человека от животных, человеческих ценностей, мотивации и воли. Люди деградируют и превращаются в диких животных, не ограниченных стремлениями к прекрасному, этикой и культурой. Всего несколько дней, и цивилизации рухнули, а мир теперь населен миллиардами агрессивных обезьян. \r\r Переждите пик активности вируса в бункере. Затем вам предстоит вернуть в мир людей культуру и спасти человечество', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Русский эпос', 'Щуку из русской сказки «заклинело» она исполняет желание каждую минуту. Люди то взмывают в небо, то проваливаются к чертовой матери, то дружно идут в баню... \r\r Спасти свою жизнь и рассудок можно только в бункере, выйдя из которого придется восстанавливать мир, порядок и психическое здоровье тех, кто остался в живых. Вам предстоит не только не потерять разум среди былинных чудес, но и придумать «культурный антидот» чтобы снять проклятие', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Страдающий Коля', 'Авария в ядерном испытательном центре привела к разлому во времени, и в наш мир попал страдающий средневековый эксгибиционист Коля. Прямо сейчас он безумно мечется и пугает людей нашего времени. Увидев Колю, люди оцепеневают и говорят «ой». Сказавших «ой» все больше и больше, Коля очень активен! \r\r Укройтесь от Коли в бункере в эпицентре событий. Затем же, набравшись смелости и завязав себе глаза, вам предстоит вернуться в мир, что бы найти Колю(с завязанными глазами, а значит на ощупь) и убедить его вернуться в средневековье', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Суицидальная фауна', 'На планете происходит аномальный эволюционный виток. Вся фауна становится смертоносной для людей. Невидимое излучение от растений и деревьев сводит людей с ума и заставляет совершать самоубийство. \r\r Бункер позволит вам пережить самую опасную фазу. Когда вы вновь выйдете на поверхность, вам нужно будет найти и уничтожить эпицентр этой аномалии и сохранить рассудок', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Спервулканы', 'Активизируются супервулканы, производящие чрезвычайно мощное извержения. Ландшафт и климат резко меняются. Большинство населения сразу погибает от скачков температур, землетрясений и наводнений. \r\r Пережив самую активную фазу в особо укрепленном бункере. Потом вас ждет глобальная засуха, разрушенные города и постоянная сейсмическая активность. Вы сможете выжить, только разработав сверхчувствительную интеллектуальную систему предсказания землетрясений и роботизированную инфраструктуру', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Химическая война', 'В результате масштабного применения химического оружия серьезно меняется экологический баланс. Нарушен микробиологический состав почв и воды, отравлены растения, погибнут почти все животные и люди. \r\r После выхода из бункера будет непросто обеспечить себе пропитание. Вам пригодятся ученые и инженеры для обустройства убежищ и ферм', '', 10, 0);
                insert into Disaster (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Ядерная война', 'Начинается масштабный ядерный конфликт. Радиоактивная пыль окутает всю планету, закрыв солнечный свет, и на земле наступит долгая ядерная зима. Почти вся территория планеты будет заражена радиацией, выживших почти не останется. \r После выхода из бункера вам предстоит обустроить постоянное убежище, обеспечить пропитание и начать восстанавливать жизнь на земле', '', 10, 0);
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
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Все спокойно', 'Вам повезло, обошлось без происшествий!', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Затопление', 'В бункер проникает вода и вас может просто затопить! \r\r Нужно перенастроить компьютерную систему управления бункера или придумать какое-то инженерное решение для откачки воды', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Землетрясение', 'Небольшое локальное землетрясение грозит смять ваш бункер и повредить системы жизнеобеспечения. \r\r Нужно экстренно провести работу по укреплению слабых мест - дверей и вентиляции. Или же останавливать землетрясение какой-то магией', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Капча', 'ИИ управления бункера ""заглючело"" и блокирует жизнеобеспечение - необходимо доказать бездушному компьютеру наличие в бункере дышащих живых людей. \r\r Тест построен на проверке уникального отличия человека от роботов - способности к творчеству. Вам нужно пройти его', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Кухонный взрыв', 'Обвал в кухонном и складском блоке. \r\r Вам помогут персонажи со знанием местности для вылазок и поиска продовольствия или же способные добывать еду в подвалах бункера - устроив ферму или охоту на крыс', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Нападение извне', 'Какие-то дикие люди ломятся в бункер, надо срочно что-то предпринять. \r\r Вам помогут персонажи с военными навыками, охоты, обустройства сигнализации и наблюдения, запасы оружия и электроники', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Нашествие крыс', 'Крысы добрались до ваших запасов провизии и наносят непоправимый ущерб. \r\r Вам помогут персонажи, способные истреблять грызунов или дополнительные ресурсы еды', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Отравление воды', 'Какой-то сбой с системой очистки воды. \r\r Поможет химическая фильтрация. Либо надо как-то добывать чистую воду на вылазках в окрестностях. Иначе вам потребуется медицинская помощь', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Призраки', 'Паранормальные явления могут нарушить систему жизнеобеспечения бункера. Вам помогут персонажи способные убедить призраков (или что бы это не было) покинуть бункер или же изгнать их', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Пси-атака', 'Вероятно какое-то излучение усиливает стресс и наводит панику. \r Нужны персонажи / снаряжение, которые могут снимать стресс', '', 9, 0);
                insert into Threat (Name, Description, ModelPath, DeckTypeId, isLocal) values ('Стресс-вырус', 'Вспышка смертельного вируса, развивающегося только на фоне стресса. \r\r Будут полезны медицинские навыки / снаряжение, а также любые способы контролировать стресс. Не нервничаем, все хорошо, мы все равно все умрем...', '', 9, 0);
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
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
