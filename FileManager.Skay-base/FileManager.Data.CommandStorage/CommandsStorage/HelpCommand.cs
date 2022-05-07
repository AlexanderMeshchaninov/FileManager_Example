using System;
using FileManager.Core.Constructor;
using FileManager.Core.Data;
using FileManager.Core.Settings;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsStorage
{
    public sealed class HelpCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }
        
        private readonly ILogger _logger;
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;
        public HelpCommand(
            ILogger logger, 
            IConstructor constructor,
            ISettings settings)
        {
            _logger = logger;
            _constructor = constructor;
            _settings = settings;
        }
        
        //Не стал сильно заморачиваться со способом вывода, можно сделать с загрузкой из файла например
        public void Execute()
        {
            _logger.Information("Help command start");
            try
            {
                _logger.Information("Help command stop");
                _constructor.SetElementPosition(_settings.MiddlePosition - 22, 3);
                _constructor.SetElement(
                    "Список доступных на данный момент комманд:\n\n" +
                    "\t\t1. '#exit$' - завершение работы консоли\n" +
                    "\t\t2. '#help$' - список комманд\n" +
                    "\t\t3. '#res$' - удаление строки поиска и возвращение к началу (если что-то пошло не так)\n" +
                    "\t\t4. '#copyfile$\\название файла c расширением' - копирование одного файла\n" +
                    "\t\t5. '#createfolder$\\название папки' - создание пустой папки\n" +
                    "\t\t6. '#deletefile$\\название файла с расширением' - удаление одного файла\n" +
                    "\t\t7. '#openfile$\\название файла с расширением' - открыть файл (если есть ассоциированная программа для этого)\n" +
                    "\t\t8. '#cd..$' - возврат на шаг назад\n" +
                    "\t\t9. '#copyallfolder$\\название папки' - копирование всей папки или директории\n" +
                    "\t\t10.'#deleteallfolder$\\название папки' - удаление всей папки (с вложенностями)/директории");
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
            }
        }
    }
}