﻿using System;
using System.Collections.Generic;

namespace TelegramBot.Infrastructure
{
    public static class JokesRepository
    {
        private static List<string> jokes = new List<string>
        {
            { @"
Свекровь говорит невестке: 
    - Живем мы хорошо, не ругаемся, все у нас есть, вот только хата не побелена! 
    - Мама, а краска есть? 
    - Краска есть, да щетки нету!
Невестка бежит к свекру, обрезает ему бороду, делает щетку и белит хату. Свекровь: 
    - Вот хата у нас теперь побелена, а окна не покрашены! 
    - Мама, а краска есть? 
    - Да есть, но кисточки нету! 
Невестка бежит к свекру, обрезает ему усы, делает кисточку и красит окна. 
Возвращается с работы муж, видит на дереве своего отца и спрашивает: 
    - Папа, что с тобой, почему ты на дереве сидишь? 
    - Сынок, да бабы собрались блины печь, а я не знаю, есть ли у них яйца!" },
            { @"
Попали на необитаемый остров американец, немец и русский. Однажды прибило к острову бутылку, открыли они ее, а оттуда - джинн: 
    - Вы меня освободили, я исполню по два ваших желания! 
    - Мешок денег и домой! - сказал американец и исчез. 
    - Кружку пива и домой! - сказал немец и был таков. 
    - Хорошая была компания, ящик водки и всех обратно! - сказал русский." },
            { "- Блин! - сказал слон, наступив на колобка." },
            { @"
Запись в школьном дневнике: 
    - Ваш Вовочка взял с собой в поход водку. Огромное спасибо!" },
            { @"
- Жесть. Ты прикинь, меня в чате забанили за то, что я там долго не появлялся! 
- Да, обнаглели! Меня в универе по этой же причине забанили..." },
            { @"
Старый профессор на экзамене: 
    - Так, кто считает, что знает предмет на 5 баллов? 
Подняло руки пара студентов. Профессор: 
    - Давайте зачётки. 
И ставит отлично. 
    - Кто считает, что знает предмет на четыре балла? 
Подняло руки человек десять. Тоже собирает зачётки, ставит четыре. 
    - Кто считает, что на 3? 
Подняли руки - та же история. 
    - Значит, так - остальные неуд, приходите на пересдачу. 
Студент: 
    - А когда пересдача? 
Профессор: 
    - Ну... давайте сейчас. Итак, кто считает, что знает предмет на 5 баллов?" },
            { @"
Студент мединститута говорит преподавателю: 
    - Профессор, мне бы зачёт поставить... Вот конфеты. 
    - Какие конфеты? Вы что? От ваших знаний зависит жизнь больного! Тут как минимум на три коньяка тянет." },
            { @"Этот студент был настолько тупой, что завалил купленный экзамен... " },
            { @"Студент, забывший дома шпаргалки, за словом в карман не полезет." }
        };

        public static string GetRandomJoke()
        {
            Random rnd = new Random();
            return jokes[rnd.Next(jokes.Count)];
        }
    }
}
