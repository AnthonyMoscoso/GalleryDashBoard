using DashboardGallery.Shared.Literals;
using DashboardGallery.Shared.Messages.Enums;
using DashboardGallery.Shared.Messages.Model;
using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Messages
{
    public partial class MessageHandler
    {
        private MessageBox MessageBox = new();
        private QuestionMessageBox QuestionMessageBox = new QuestionMessageBox();
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public EventCallback OnMessageQuestionConfirmed { get; set; }
        [CascadingParameter] LiteralsManager? Literals { get; set; }
        

        public async Task ShowMessage(string message, string tittle,string buttonName = "", MessageTypes messageTypes = MessageTypes.@default)
        {
            buttonName = string.IsNullOrWhiteSpace(buttonName) ? Literals!.Continue : buttonName;
            await MessageBox.Show(message, tittle, buttonName, GetConfig(messageTypes));

        }

        private static MessageBoxConfig GetConfig(MessageTypes messageTypes)
        {
            switch (messageTypes)
            {
                case MessageTypes.info:
                    return new()
                    {
                        TitleColor = Color.Success.ToString(),
                        ButtonColor = Color.Success.ToString(),
                        BorderColor = Color.Success.ToString(),
                        ImageUrl = "https://media4.giphy.com/media/v1.Y2lkPTc5MGI3NjExM2g4bmQ0ZTRudWNjaHNsemsyc2l5Ym5mOWdhMjlqYmw4cnN1MWl1dSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9cw/g19ZBWCQtWllHWZp5S/giphy.gif"
                    };

                case MessageTypes.warning:
                    return new()
                    {

                        TitleColor = "#FF5757",
                        ButtonColor = "#FF5757",
                        BorderColor = "#FF5757",
                        ImageUrl = "https://media2.giphy.com/media/MaEHwVOESdbRwrF971/giphy.gif?cid=ecf05e47venlxr2ubdg614hbmjbln6v3i0xgsikje3lzf5t6&ep=v1_stickers_search&rid=giphy.gif&ct=s"
                    };

                case MessageTypes.forbidden:
                    return new()
                    {
                        BackgroundColor = "#CC5527",
                        TitleColor = "#F4F4F4",
                        ButtonColor = "#FF914D",
                        ButtonBoderColor = "#FFFFFF",
                        ButtonBoder = 2,
                        BorderColor = "transparent",
                        TextColor = "#F4F4F4",
                        ImageUrl = "https://media2.giphy.com/media/v1.Y2lkPTc5MGI3NjExdTFsNnJlemplMnlvYzE2MGw2YXZmeXdkaHc5cm1td3l0OGt0eGx6biZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9cw/UXJ2WE5n48JVrKQ2Q4/giphy.gif"
                    };
                case MessageTypes.@default:
                    return new()
                    {
                        ImageUrl = "https://media4.giphy.com/media/v1.Y2lkPTc5MGI3NjExM2g4bmQ0ZTRudWNjaHNsemsyc2l5Ym5mOWdhMjlqYmw4cnN1MWl1dSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9cw/g19ZBWCQtWllHWZp5S/giphy.gif"
                    };
                default:
                    return new()
                    {
                        ImageUrl = "https://media4.giphy.com/media/v1.Y2lkPTc5MGI3NjExM2g4bmQ0ZTRudWNjaHNsemsyc2l5Ym5mOWdhMjlqYmw4cnN1MWl1dSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9cw/g19ZBWCQtWllHWZp5S/giphy.gif"
                    };

            }



        }
        public async Task ShowQuestion(string message, string tittle, string firstOption = "", string secondOption = "", QuestionMessageType questionMessageType = QuestionMessageType.@default)
        {
            firstOption = string.IsNullOrWhiteSpace(firstOption) ? Literals!.Confirmed : firstOption;
            secondOption = string.IsNullOrWhiteSpace(secondOption) ? Literals!.Cancel : secondOption;
            await QuestionMessageBox.Show(tittle, message, firstOption, secondOption, GetConfig(questionMessageType));
        }


        public async Task ShowSuccess(string tittle, string message)
        {
            await MessageBox.Show(message, tittle,Literals!.Continue, messaBoxConfig: GetConfig(MessageTypes.info));
        }
        private static QuestionMessageBoxConfig GetConfig(QuestionMessageType questionMessageType)
        {
            switch (questionMessageType)
            {
                case  QuestionMessageType.Unsaves:
                    return new()
                    {
                        SecondButtonColor = "#154854",
                        FirstButtonColor = "#FF0000",
                        BorderColor = "#FF0000",
                        TitleColor = "#FF0000",
                        ImageUrl = "https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExZTkxNDZiMmUyYjI1NzNlZjI4MGIzYjhjYjZhZTAzZjRlMGEwYTA4MyZlcD12MV9pbnRlcm5hbF9naWZzX2dpZklkJmN0PXM/DTuTMRAQ4EVGBbtpkg/giphy.gif"
                    };
                case QuestionMessageType.Delete:
                    return new()
                    {
                        SecondButtonColor = Color.Black.ToString(),
                        FirstButtonColor = "#FF0000",
                        BorderColor = "#FF0000",
                        TitleColor = "#FF0000",
                        ImageUrl = "https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExZTcyOTQ3ZDQzZTRhYzAzYmRlZDdkOTIzNzljMTZlM2ZkOGQwMjZiYSZlcD12MV9pbnRlcm5hbF9naWZzX2dpZklkJmN0PXM/f9ZDFh7l8EdESPCVee/giphy.gif"
                    };
                    case QuestionMessageType.@default:
                   
                default:
                    return new()
                    {
                        ImageUrl = "https://media2.giphy.com/media/1BURfsUHbv4eQ/200.webp?cid=ecf05e47rsuo7qlzlfr10p6s6daxrzmf11nmnne38n2itgyy&ep=v1_stickers_search&rid=200.webp&ct=s"
                    };
            }
        }

        public async Task ShowMessage(string message, string tittle,string buttonName = "",MessageBoxConfig? messageBoxConfig = null)
        {
            buttonName = string.IsNullOrWhiteSpace(buttonName) ? Literals!.Continue : buttonName;
            await MessageBox.Show(message, tittle ,buttonName,messageBoxConfig);
        }

        private async Task OnOption1Confirmed()
        {
            await OnMessageQuestionConfirmed.InvokeAsync();
        }

        private async Task OnOption2Confirmed()
        {
            await OnMessageQuestionConfirmed.InvokeAsync();
        }
        public async Task Close()
        {
            await MessageBox.Hide();
        }


    }
}
