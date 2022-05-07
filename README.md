# SKAY-BASE_Example v1.0
Данный файловый менеджер имеет базовые команды для управления файловой системой.
!Для перехода в режим команд нужно набрать cmd!
Список доступных на данный момент комманд:

 - '#exit$' - завершение работы консоли (с сохранением текущей позиции);
 - '#help$' - список комманд;
 - '#res$' - удаление строки поиска и возвращение к началу (если что-то пошло не так);
 - '#copyfile$\название файла c расширением' - копирование одного файла;
 - '#createfolder$\название папки' - создание пустой папки;
 - '#deletefile$\название файла с расширением' - удаление одного файла;
 - '#openfile$\название файла с расширением' - открыть файл (если есть ассоциированная программа для этого);
 - '#cd..$' - возврат на шаг назад;
 - '#copyallfolder$\название папки' - копирование всей папки или директории;
 - '#deleteallfolder$\название папки' - удаление всей папки (с вложенностями)/директории;

Первоначальное меню с отображение доступных носителей информации.
 "Menu"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/Menu.png "Menu")

Пример использования комманд в файл менеджере:

Создадим в директории рабочего стола (ОС Windows 11) пустую папку. Набираем cmd, далее комманду #createfolder$\MYNEWFOLDER, где MYNEWFOLDER название папки.

 "CreateFolderCommand"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/CreateFolderCommand.png "CreateFolderCommand")

Далее после ввода команды, появляется информационное сообщение с подтверждением создания папки.
 "MenuFolderCreationCommand"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/MenuOfFolderCreation.png "MenuFolderCreationCommand")

Проверяем, папка успешно создана.
 "FolderSuccessfullyCreated"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/FolderCreatedSuccessfully.png "FolderSuccessfullyCreated")

Далее попробуем копировать папку ScreenShots (ипользуется для примера) в новую созданную нами директорию MYNEWFOLDER.
Набираем cmd, далее #copyallfolder$\ScreenShots, где ScreenShots, название той папки, которую хотим копировать.
 "CopyAllFolderCommand"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/CopyAllFolderCommand.png "CopyAllFolderCommand")

Далее появляется информационное сообщение с указанием директорий откуда копируется папку и куда.
 "Menu1CopyAllFolder"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/Menu2OfCopyAllFolder.png "Menu1CopyAllFolder")

В строке TO: мы указываем лишь директорию где хранится папка MYNEWFOLDER (в данном случае она лежит в директории Desktop "Рабочий стол"), а не саму папку, т.к. следующее информационное сообщение будет с вопросом о названии папки. Набираем имя папки MYNEWFOLDER.
 "Menu2CopyAllFolder"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/Menu3OfCopyAllFolder.png "Menu2CopyAllFolder")

Проверяем копировались ли файлы из папки ScreenShots в папку MYNEWFOLDER
 "CheckMYNEWFOLDERDirectory"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/MYNEWFOLDER_Direction.png "CheckMYNEWFOLDERDirectory")

Все прошло успешно и все файлы копировались.
Далее попробуем удалить из папки MYNEWFOLDER один файл с названием C_Direction.png. Набираем cmd, далее #deletefile$\С_Direction.png, где С_Direction.png название удаляемого файла.
 "DeleteFileCommand"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/DeleteFileCommand.png "DeleteFileCommand")

После ввода команды появляется стандартное информационное сообщение.
 "MenuDeleteFile"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/MenuOfDeletingFile.png "MenuDeleteFile")

Проверяем удалился ли указанный файл.
 "CheckMYNEWFOLDERDirectory"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/CheckMYNEWFOLDER_Direction.png "CheckMYNEWFOLDERDirectory")

Файл успешно удален.
Теперь попробуем удалить всю папку MYNEWFOLDER. Для этого, как обычно набираем cmd и вводим новую команду #deleteallfolder$\MYNEWFOLDER, где MYNEWFOLDER название удаляемой папки.
 "DeleteAllFolderCommand"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/DeleteAllFolderCommand.png "DeleteAllFolderCommand")

Стандартное сообщение.
 "Menu1DeleteAllFolder"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/MenuOfDeletingAllFolder.png "MenuDeleteAllFolder")

Проверяем удалилась ли папка MYNEWFOLDER.
 "CheckDesktopToDeleteFolder"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/CheckDesktop_Direction.png "CheckDesktopToDeleteFolder")
