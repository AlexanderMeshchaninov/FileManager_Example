namespace FileManager.Core.Data
{
    public interface ICommandsMessages
    {
        void FromToMessage(string currentDirectory); 
        void FolderOrFileNameMessage(string folderOrFileName); 
        void CopySuccessMessage(string folderOrFileName); 
        void DeleteSuccessMessage(string folderOrFileName); 
        void NotCopiedMessage(string folderOrFileName); 
        void NotDeletedMessage(string folderOrFileName); 
        void WrongDestinationMessage(); 
        void CopyQuestionMessage(string folderOrFileName); 
        void DeleteQuestionMessage(string folderOrFileName);
        void InProgressMessage();
        void OpenQuestionMessage(string folderOrFileName);
        void OpenSuccessMessage(string folderOrFileName);
        void NotOpenedMessage(string folderOrFileName);
    }
}
        
    