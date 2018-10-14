using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        private const string ExitComand = "exit";
        private const string StopDownloadComand = "stop";
        private const string ShowListOfWebPages = "list";
        private const string StartComand = "start";
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private static CancellationToken token = tokenSource.Token;
        private static List<WebPage> webPages;
        private static bool exit;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter an URL to download a page or enter the following commands:\n1. 'exit' to finish the program\n2. 'stop' to stop current downloading\n3. 'list' to show the list of web pages\n4. 'start' to start new downloading");

            webPages = CreateFakeWebPages();

            while(!exit)
            {
                DoSomethingAsync();
            }

            Console.WriteLine("\n\nThe exit was requested.\nTap to continue...");
            Console.ReadKey();
        }

        private static async void DoSomethingAsync()
        {
            string input = await ReadInputAsync();

            if(input == ExitComand)
            {
                exit = true;
                return;
            }

            if(input == StopDownloadComand)
            {
                Console.WriteLine("Cancellation of downloading was requested.");
                tokenSource.Cancel();
                return;
            }

            if(input == ShowListOfWebPages)
            {
                await PrintListOfWebPagesAsync();
                return;
            }

            if(input == StartComand)
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                return;
            }

            string content = await GetContentAsync(input);

            if(string.IsNullOrEmpty(content))
            {
                Console.WriteLine($"Page [{input}] was not found.");
            }
            else
            {
                Console.WriteLine($"\n\n{content}\n\n");
            }
        }

        private static Task<string> GetContentAsync(string url)
        {
            return Task<string>.Run(() => {
                for (int i = 0; i < 50; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        return string.Empty;
                    }

                    Thread.Sleep(100);
                }

                var page = webPages.FirstOrDefault(p => p.Url == url);

                return page == null ? string.Empty : page.ToString();
            });
        }

        /// <summary>
        /// Reads an input async.
        /// </summary>
        /// <returns>The string representation of an input.</returns>
        private static Task<string> ReadInputAsync()
        {
            return Task<string>.Run(() => Console.ReadLine());
        }

        private static List<WebPage> CreateFakeWebPages()
        {
            return new List<WebPage>
            {
                new WebPage
                {
                    Url = "www.tut.by",
                    Title = "TUT.BY News",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed arcu arcu, gravida at lacus quis, sodales tristique nisl. Donec auctor ex in efficitur placerat. Quisque laoreet convallis fermentum. Nunc eu lorem non nisi suscipit auctor et sit amet leo. Duis ipsum magna, pellentesque eu tempus quis, ultricies sit amet nulla. Phasellus cursus tellus sed lorem finibus consectetur. Cras vitae lacus eu risus posuere dapibus quis non lacus. In hac habitasse platea dictumst. In hac habitasse platea dictumst. Cras viverra magna diam. Donec semper diam vel lacus egestas, eget dapibus mauris pulvinar. Pellentesque ac sapien eget erat imperdiet tristique.",
                    Footer = "Copyright (c) 2018 TUT.BY"
                },
                new WebPage
                {
                    Url = "www.onliner.by",
                    Title = "Onliner",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed arcu arcu, gravida at lacus quis, sodales tristique nisl. Donec auctor ex in efficitur placerat. Quisque laoreet convallis fermentum. Nunc eu lorem non nisi suscipit auctor et sit amet leo. Duis ipsum magna, pellentesque eu tempus quis, ultricies sit amet nulla. Phasellus cursus tellus sed lorem finibus consectetur. Cras vitae lacus eu risus posuere dapibus quis non lacus. In hac habitasse platea dictumst. In hac habitasse platea dictumst. Cras viverra magna diam. Donec semper diam vel lacus egestas, eget dapibus mauris pulvinar. Pellentesque ac sapien eget erat imperdiet tristique.",
                    Footer = "Copyright (c) 2018 Onliner"
                },
                new WebPage
                {
                    Url = "www.habr.com",
                    Title = "Habr Tech News",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed arcu arcu, gravida at lacus quis, sodales tristique nisl. Donec auctor ex in efficitur placerat. Quisque laoreet convallis fermentum. Nunc eu lorem non nisi suscipit auctor et sit amet leo. Duis ipsum magna, pellentesque eu tempus quis, ultricies sit amet nulla. Phasellus cursus tellus sed lorem finibus consectetur. Cras vitae lacus eu risus posuere dapibus quis non lacus. In hac habitasse platea dictumst. In hac habitasse platea dictumst. Cras viverra magna diam. Donec semper diam vel lacus egestas, eget dapibus mauris pulvinar. Pellentesque ac sapien eget erat imperdiet tristique.",
                    Footer = "Copyright (c) 2018 Habr"
                },
                new WebPage
                {
                    Url = "www.wiki.org",
                    Title = "Wikipedia, the free encyclopedia",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed arcu arcu, gravida at lacus quis, sodales tristique nisl. Donec auctor ex in efficitur placerat. Quisque laoreet convallis fermentum. Nunc eu lorem non nisi suscipit auctor et sit amet leo. Duis ipsum magna, pellentesque eu tempus quis, ultricies sit amet nulla. Phasellus cursus tellus sed lorem finibus consectetur. Cras vitae lacus eu risus posuere dapibus quis non lacus. In hac habitasse platea dictumst. In hac habitasse platea dictumst. Cras viverra magna diam. Donec semper diam vel lacus egestas, eget dapibus mauris pulvinar. Pellentesque ac sapien eget erat imperdiet tristique.",
                    Footer = "Copyright (c) 2018 Wikipedia"
                },
            };
        }

        private static Task PrintListOfWebPagesAsync()
        {
            return Task.Run(() =>
             {
                 var sb = new StringBuilder();

                 foreach (var item in webPages)
                 {
                     sb.Append(item.Url + Environment.NewLine);
                 }

                 Console.WriteLine(sb.ToString());
             });
        }
    }
}
