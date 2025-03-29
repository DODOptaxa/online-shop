namespace Store.Contractors
{
    public class NovaPoshtaPostmateDeliveryService : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>
{
    { "1", "Київ" },
    { "2", "Харків" },
    { "3", "Одеса" },
    { "4", "Дніпро" },
    { "5", "Львів" },
    { "6", "Запоріжжя" },
    { "7", "Кривий Ріг" },
    { "8", "Миколаїв" },
    { "9", "Вінниця" },
    { "10", "Херсон" },
    { "11", "Полтава" },
    { "12", "Черкаси" },
    { "13", "Хмельницький" },
    { "14", "Чернівці" },
    { "15", "Суми" },
    { "16", "Рівне" },
    { "17", "Івано-Франківськ" },
    { "18", "Тернопіль" },
    { "19", "Луцьк" },
    { "20", "Ужгород" },
    { "21", "Житомир" },
    { "22", "Кропивницький" },
    { "23", "Чернігів" },
    { "24", "Біла Церква" },
    { "25", "Мелітополь" },
    { "26", "Кам'янець-Подільський" },
    { "27", "Кременчук" },
    { "28", "Бровари" },
    { "29", "Маріуполь" },
    { "30", "Павлоград" },
    { "31", "Сєвєродонецьк" },
    { "32", "Лисичанськ" },
    { "33", "Краматорськ" },
    { "34", "Слов'янськ" },
    { "35", "Нікополь" },
    { "36", "Бердянськ" },
    { "37", "Енергодар" },
    { "38", "Шостка" },
    { "39", "Конотоп" },
    { "40", "Прилуки" },
    { "41", "Умань" },
    { "42", "Мукачево" },
    { "43", "Дрогобич" },
    { "44", "Стрий" },
    { "45", "Коломия" },
    { "46", "Нововолинськ" },
    { "47", "Бориспіль" },
    { "48", "Ірпінь" },
    { "49", "Фастів" },
    { "50", "Вишгород" },
    { "51", "Олександрія" },
    { "52", "Сміла" }
};

        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postamates =
            new Dictionary<string, IReadOnlyDictionary<string, string>>
            {
        { "1", new Dictionary<string, string>
            {
                { "1", "Київ, вул. Пирогівський шлях, 135" },
                { "2", "Київ, вул. Слобожанська, 13" },
                { "3", "Київ, вул. Хрещатик, 22" }
            }
        },
        { "2", new Dictionary<string, string>
            {
                { "1", "Харків, вул. Польова, 67" },
                { "2", "Харків, вул. Достоєвського, 5" },
                { "3", "Харків, вул. Сумська, 10" }
            }
        },
        { "3", new Dictionary<string, string>
            {
                { "1", "Одеса, вул. Дерибасівська, 15" },
                { "2", "Одеса, вул. Грецька, 45" },
                { "3", "Одеса, вул. Приморська, 27" }
            }
        },
        { "4", new Dictionary<string, string>
            {
                { "1", "Дніпро, вул. Січеславська Набережна, 33" },
                { "2", "Дніпро, вул. Глінки, 2" }
            }
        },
        { "5", new Dictionary<string, string>
            {
                { "1", "Львів, вул. Свободи, 28" },
                { "2", "Львів, вул. Городоцька, 15" },
                { "3", "Львів, вул. Шевченка, 60" }
            }
        },
        { "6", new Dictionary<string, string>
            {
                { "1", "Запоріжжя, вул. Соборна, 150" },
                { "2", "Запоріжжя, вул. Металургів, 8" }
            }
        },
        { "7", new Dictionary<string, string>
            {
                { "1", "Кривий Ріг, вул. Героїв АТО, 30" },
                { "2", "Кривий Ріг, вул. Вечірній бульвар, 2" }
            }
        },
        { "8", new Dictionary<string, string>
            {
                { "1", "Миколаїв, вул. Центральна, 71" },
                { "2", "Миколаїв, вул. Адміральська, 12" }
            }
        },
        { "9", new Dictionary<string, string>
            {
                { "1", "Вінниця, вул. Соборна, 24" },
                { "2", "Вінниця, вул. Пирогова, 31" }
            }
        },
        { "10", new Dictionary<string, string>
            {
                { "1", "Херсон, вул. Ушакова, 35" },
                { "2", "Херсон, вул. Перекопська, 20" }
            }
        },
        { "11", new Dictionary<string, string>
            {
                { "1", "Полтава, вул. Європейська, 60" },
                { "2", "Полтава, вул. Соборності, 45" }
            }
        },
        { "12", new Dictionary<string, string>
            {
                { "1", "Черкаси, вул. Смілянська, 44" },
                { "2", "Черкаси, вул. Шевченка, 250" }
            }
        },
        { "13", new Dictionary<string, string>
            {
                { "1", "Хмельницький, вул. Проскурівська, 45" },
                { "2", "Хмельницький, вул. Кам'янецька, 17" }
            }
        },
        { "14", new Dictionary<string, string>
            {
                { "1", "Чернівці, вул. Головна, 122" },
                { "2", "Чернівці, вул. Університетська, 10" }
            }
        },
        { "15", new Dictionary<string, string>
            {
                { "1", "Суми, вул. Соборна, 36" },
                { "2", "Суми, вул. Харківська, 5" }
            }
        },
        { "16", new Dictionary<string, string>
            {
                { "1", "Рівне, вул. Соборна, 94" },
                { "2", "Рівне, вул. Київська, 18" }
            }
        },
        { "17", new Dictionary<string, string>
            {
                { "1", "Івано-Франківськ, вул. Незалежності, 87" },
                { "2", "Івано-Франківськ, вул. Галицька, 32" }
            }
        },
        { "18", new Dictionary<string, string>
            {
                { "1", "Тернопіль, вул. Руська, 21" },
                { "2", "Тернопіль, вул. Шевченка, 12" }
            }
        },
        { "19", new Dictionary<string, string>
            {
                { "1", "Луцьк, вул. Лесі Українки, 52" },
                { "2", "Луцьк, вул. Винниченка, 4" }
            }
        },
        { "20", new Dictionary<string, string>
            {
                { "1", "Ужгород, вул. Корятовича, 5" },
                { "2", "Ужгород, вул. Собранецька, 89" }
            }
        },
        { "21", new Dictionary<string, string>
            {
                { "1", "Житомир, вул. Київська, 77" },
                { "2", "Житомир, вул. Перемоги, 10" }
            }
        },
        { "22", new Dictionary<string, string>
            {
                { "1", "Кропивницький, вул. Велика Перспективна, 48" },
                { "2", "Кропивницький, вул. Шевченка, 15" }
            }
        },
        { "23", new Dictionary<string, string>
            {
                { "1", "Чернігів, вул. Миру, 35" },
                { "2", "Чернігів, вул. Шевченка, 22" }
            }
        },
        { "24", new Dictionary<string, string>
            {
                { "1", "Біла Церква, вул. Ярослава Мудрого, 40" },
                { "2", "Біла Церква, вул. Леваневського, 53" }
            }
        },
        { "25", new Dictionary<string, string>
            {
                { "1", "Мелітополь, вул. Героїв України, 14" },
                { "2", "Мелітополь, вул. Університетська, 7" }
            }
        },
        { "26", new Dictionary<string, string>
            {
                { "1", "Кам'янець-Подільський, вул. Соборна, 29" },
                { "2", "Кам'янець-Подільський, вул. Лесі Українки, 11" }
            }
        },
        { "27", new Dictionary<string, string>
            {
                { "1", "Кременчук, вул. Соборна, 23" },
                { "2", "Кременчук, вул. Халаменюка, 8" }
            }
        },
        { "28", new Dictionary<string, string>
            {
                { "1", "Бровари, вул. Київська, 316" },
                { "2", "Бровари, вул. Гагаріна, 16" }
            }
        },
        { "29", new Dictionary<string, string>
            {
                { "1", "Маріуполь, вул. Металургів, 112" },
                { "2", "Маріуполь, вул. Миру, 69" }
            }
        },
        { "30", new Dictionary<string, string>
            {
                { "1", "Павлоград, вул. Соборна, 56" },
                { "2", "Павлоград, вул. Центральна, 90" }
            }
        },
        { "31", new Dictionary<string, string>
            {
                { "1", "Сєвєродонецьк, вул. Гагаріна, 12" },
                { "2", "Сєвєродонецьк, вул. Центральна, 45" }
            }
        },
        { "32", new Dictionary<string, string>
            {
                { "1", "Лисичанськ, вул. Сосюри, 15" },
                { "2", "Лисичанськ, вул. Перемоги, 20" }
            }
        },
        { "33", new Dictionary<string, string>
            {
                { "1", "Краматорськ, вул. Паркова, 18" },
                { "2", "Краматорськ, вул. Машинобудівників, 5" }
            }
        },
        { "34", new Dictionary<string, string>
            {
                { "1", "Слов'янськ, вул. Шевченка, 30" },
                { "2", "Слов'янськ, вул. Університетська, 12" }
            }
        },
        { "35", new Dictionary<string, string>
            {
                { "1", "Нікополь, вул. Електрометалургів, 42" },
                { "2", "Нікополь, вул. Шевченка, 188" }
            }
        },
        { "36", new Dictionary<string, string>
            {
                { "1", "Бердянськ, вул. Свободи, 27" },
                { "2", "Бердянськ, вул. Приморська, 13" }
            }
        },
        { "37", new Dictionary<string, string>
            {
                { "1", "Енергодар, вул. Молодіжна, 10" },
                { "2", "Енергодар, вул. Козацька, 5" }
            }
        },
        { "38", new Dictionary<string, string>
            {
                { "1", "Шостка, вул. Свободи, 39" },
                { "2", "Шостка, вул. Чернігівська, 8" }
            }
        },
        { "39", new Dictionary<string, string>
            {
                { "1", "Конотоп, вул. Успенсько-Троїцька, 15" },
                { "2", "Конотоп, вул. Шевченка, 70" }
            }
        },
        { "40", new Dictionary<string, string>
            {
                { "1", "Прилуки, вул. Київська, 132" },
                { "2", "Прилуки, вул. Незалежності, 25" }
            }
        },
        { "41", new Dictionary<string, string>
            {
                { "1", "Умань, вул. Європейська, 31" },
                { "2", "Умань, вул. Шевченка, 4" }
            }
        },
        { "42", new Dictionary<string, string>
            {
                { "1", "Мукачево, вул. Миру, 12" },
                { "2", "Мукачево, вул. Духновича, 89" }
            }
        },
        { "43", new Dictionary<string, string>
            {
                { "1", "Дрогобич, вул. Шевченка, 15" },
                { "2", "Дрогобич, вул. Стрийська, 22" }
            }
        },
        { "44", new Dictionary<string, string>
            {
                { "1", "Стрий, вул. Шевченка, 71" },
                { "2", "Стрий, вул. Олесницького, 8" }
            }
        },
        { "45", new Dictionary<string, string>
            {
                { "1", "Коломия, вул. Театральна, 12" },
                { "2", "Коломия, вул. Мазепи, 36" }
            }
        },
        { "46", new Dictionary<string, string>
            {
                { "1", "Нововолинськ, вул. Свободи, 5" },
                { "2", "Нововолинськ, вул. Шахтарська, 10" }
            }
        },
        { "47", new Dictionary<string, string>
            {
                { "1", "Бориспіль, вул. Київський шлях, 79" },
                { "2", "Бориспіль, вул. Соборна, 2" }
            }
        },
        { "48", new Dictionary<string, string>
            {
                { "1", "Ірпінь, вул. Шевченка, 4" },
                { "2", "Ірпінь, вул. Центральна, 25" }
            }
        },
        { "49", new Dictionary<string, string>
            {
                { "1", "Фастів, вул. Соборна, 17" },
                { "2", "Фастів, вул. Шевченка, 8" }
            }
        },
        { "50", new Dictionary<string, string>
            {
                { "1", "Вишгород, вул. Шолуденка, 6" },
                { "2", "Вишгород, вул. Набережна, 11" }
            }
        },
        { "51", new Dictionary<string, string>
            {
                { "1", "Олександрія, вул. Соборна, 59" },
                { "2", "Олександрія, вул. Героїв Сталинграда, 15" }
            }
        },
        { "52", new Dictionary<string, string>
            {
                { "1", "Сміла, вул. Соборна, 77" },
                { "2", "Сміла, вул. Незалежності, 12" }
            }
        }
            };

        public string Code => "Postmate";

        public string Title => "Поштомат Нова Пошта";

        public OrderDelivery CreateDelivery(Form form)
        {
            if (Code != form.Code)
                throw new ArgumentException("Ivalid form");

            var cityId = form.Fields.Single(field => field.Name == "city").Value;
            var cityName = cities[cityId];
            var postamateId = form.Fields.
                              Single(field => field.Name == "postamate")
                              .Value;

            var postamateName = postamates[cityId][postamateId];


            var parameters = new Dictionary<string, string>()
            {
                {nameof(cityId), cityId},
                {nameof(cityName), cityName},
                {nameof(postamateId), postamateId},
                {nameof(postamateName), postamateName},
            };
            var description = $"Город: {cityName}\nПостамат: {postamateName}";
            return new OrderDelivery(Code, description, 150m, parameters);
        }

        public Form CreateForm(int orderId)
        {
            return new Form(Code, orderId, new[]
            {
                new Field("Город", "city", "1", false ,cities),
                new Field("Поштомат", "postamate", "1", false , postamates["1"]),
            });
        }
        public Form CreateUpdatedForm(int orderId, IReadOnlyDictionary<string, string> values)
        {
                if (values["city"] != null && values["postamate"] != null)
                {
                    return new Form(Code, orderId, new Field[]
                    {
                        new Field("Город", "city", values["city"],true, cities),
                        new Field("Постамат", "postamate", values["postamate"],true, postamates[values["city"]]),
                    });
                }
                else
                    throw new InvalidOperationException("Invalid postamate or city.");
            }
    }
}
