using System;
using System.Threading;
using NUnit.Framework;
using TelegramBot.Commands;
using TelegramBot.Parsers;
using TelegramBot.Writers;
using TelegramBot.Infrastructure;

namespace TelegramBot.Tests
{
    [TestFixture]
    public class CommandsTests
    {
        static ScheduleParser parser = new ScheduleParser();
        ScheduleCommand scheduleCommand = new ScheduleCommand(
            parser,
            new LongTextWriter(null, new Lazy<ICommandsExecutor>(() => new CommandsExecutor(new MyBotCommand[0]))));
        public TomatoTimerCommand tomatoCommands = new TomatoTimerCommand(
            new LongTextWriter(null, new Lazy<ICommandsExecutor>(() => new CommandsExecutor(new MyBotCommand[0]))),
            new TomatoTimer());
        [Test]
        public void CorrectFirstAnswerScheduleTest()
        {
            Assert.AreEqual("Введите номер группы", scheduleCommand.Execute("расписание", null, new CancellationToken(), null));
        }

        [Test]
        public void WrongGroupWhenEmptyStringScheduleTest()
        {
            Assert.AreEqual(
                "Группа не введена",
                scheduleCommand.Execute("", null, new CancellationToken(), null));
        }

        [Test]
        public void WrongGroupWhenNullScheduleTest()
        {
            Assert.AreEqual(
                "Группа не введена",
                scheduleCommand.Execute(null, null, new CancellationToken(), null));
        }

        [Test]
        public void ReturnsScheduleScheduleTest()
        {
            scheduleCommand.Execute("расписание", null, new CancellationToken(), null);
            var response = scheduleCommand.Execute("РИ-390013", null, new CancellationToken(), null);
            Assert.AreNotEqual("Группа не введена",
                response);
            Assert.AreNotEqual("Введите группу",
                response);
        }

        [Test]
        public void TomatoStartCommandTest()
        {
            var caseStart = tomatoCommands.Execute("Tomato Start", null, new CancellationToken(), null);
            Assert.AreEqual("Время работать!, 25 минут", caseStart);
        }

        [Test]
        public void TomatoStopCommandTest()
        {
            var caseStop = tomatoCommands.Execute("Tomato Stop", null, new CancellationToken(), null);
            Assert.AreEqual("Таймер остановлен", caseStop);
        }

        [Test]
        public void TomatoUnknownCommandTest()
        {
            var caseUnknown = tomatoCommands.Execute("Tomato Stratp", null, new CancellationToken(), null);
            Assert.AreEqual("Неизвестная команда таймера", caseUnknown);
        }

        [Test]
        public void TomatoShowCommandsTest()
        {
            var caseCommands = tomatoCommands.Execute("", null, new CancellationToken(), null);
            Assert.AreEqual("Допустимые команды: \n Tomato Start - Запуск таймера \n Tomato Stop - Остановка таймера", caseCommands);
        }

        [Test]
        public void TomatoNullCommandTest()
        {
            var caseCommands = tomatoCommands.Execute(null, null, new CancellationToken(), null);
            Assert.AreEqual("Допустимые команды: \n Tomato Start - Запуск таймера \n Tomato Stop - Остановка таймера", caseCommands);
        }

        [Test]
        public void TomatoEmptyCommandTest()
        {
            var caseCommands = tomatoCommands.Execute("", null, new CancellationToken(), null);
            Assert.AreEqual("Допустимые команды: \n Tomato Start - Запуск таймера \n Tomato Stop - Остановка таймера", caseCommands);
        }

        [Test]
        public void TomatoAlreadyStartTest()
        {
            var caseCommands = tomatoCommands.Execute("Tomato Start", null, new CancellationToken(), null);
            var caseCommandsAgain = tomatoCommands.Execute("Tomato Start", null, new CancellationToken(), null);
            Assert.AreEqual("Таймер уже запущен", caseCommandsAgain);
        }
    }

    [TestFixture]
    public class TomatoTest
    {
        public TomatoTimerStateEnum TomatoTimerState { get; private set; }

        [Test]
        public void StateFirstWorkTest()
        {
            TomatoTimer tomato = new TomatoTimer(0.01, 0.01, 0.01);
            tomato.StartTimer();
            TomatoTimerState = TomatoTimerStateEnum.Work;
            Assert.AreEqual(TomatoTimerState, tomato.TomatoTimerState);
        }

        [Test]
        public void StateShortChillTest()
        {
            TomatoTimer tomato = new TomatoTimer(0.01, 0.01, 0.01);
            TomatoTimerState = TomatoTimerStateEnum.ShortChill;
            tomato.StartTimer();
            Thread.Sleep(700);
            tomato.StartTimer();
            Assert.AreEqual(TomatoTimerState, tomato.TomatoTimerState);
        }

        [Test]
        public void StateSecondWorkTest()
        {
            TomatoTimer tomato = new TomatoTimer(0.01, 0.01, 0.01);
            tomato.StartTimer();
            TomatoTimerState = TomatoTimerStateEnum.Work;
            Thread.Sleep(1300);
            Assert.AreEqual(TomatoTimerState, tomato.TomatoTimerState);
        }

        [Test]
        public void StateLongChillTest()
        {
            TomatoTimer tomato = new TomatoTimer(0.01, 0.01, 0.01);
            tomato.StartTimer();
            TomatoTimerState = TomatoTimerStateEnum.LongChill;
            Thread.Sleep(1900);
            Assert.AreEqual(TomatoTimerState, tomato.TomatoTimerState);
        }

        [Test]
        public void StateWorkAfterLongChillTest()
        {
            TomatoTimer tomato = new TomatoTimer(0.01, 0.01, 0.01);
            tomato.StartTimer();
            TomatoTimerState = TomatoTimerStateEnum.Work;
            Thread.Sleep(2500);
            Assert.AreEqual(TomatoTimerState, tomato.TomatoTimerState);
        }
    }
}
