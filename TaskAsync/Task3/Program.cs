using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        private static object lockObject = new object();
        private static List<Product> products;
        private static ShoppingCart shoppingCart;
        private static bool exit;

        static void Main(string[] args)
        {
            products = CreateProducts();
            shoppingCart = new ShoppingCart();

            Console.WriteLine("You can use one of the following commands:");
            PrintListOfCommandsToConsole();

            while (!exit)
            {
                Start();
            }

            Console.WriteLine("\n\nThe exit was requested.");
            Console.WriteLine("Tap to continue...");
        }

        private static async void Start()
        {
            var command = await ReadInputCommandAsync();

            switch (command.Name)
            {
                case "add":
                    await AddItemToShoppingCartAsync(command.Id);
                    double totalAmountAfterAdding = await CalculateTotalAmountAsync();
                    Console.WriteLine($"Total amount: {totalAmountAfterAdding}");
                    break;
                case "del":
                    await RemoveItemFromShoppingCartAsync(command.Id);
                    double totalAmountAfterRemoving = await CalculateTotalAmountAsync();
                    Console.WriteLine($"Total amount: {totalAmountAfterRemoving}");
                    break;
                case "clear":
                    await ClearShoppingCartAsync();
                    break;
                case "stop":
                    exit = true;
                    break;
                case "cmd":
                    PrintListOfCommandsToConsole();
                    break;
                case "show":
                    PrintListOfProductsToConsole();
                    break;
                case "unknown":
                    Console.WriteLine("You entered an unknown command.");
                    break;
            }
        }

        private static async Task<Command> ReadInputCommandAsync()
        {
            return await Task<Command>.Run(() => {
                string input = Console.ReadLine();
                var words = input.Split(' ');
                int id = 0;

                if(words.Length == 2)
                {
                    Int32.TryParse(words[1], out id);
                }

                switch(words[0])
                {
                    case "add":
                        return new Command
                        {
                            CommandId = 1,
                            Name = "add",
                            Id = id
                        };
                    case "del":
                        return new Command
                        {
                            CommandId = 2,
                            Name = "del",
                            Id = id
                        };
                    case "clear":
                        return new Command
                        {
                            CommandId = 3,
                            Name = "clear"
                        };
                    case "stop":
                        return new Command
                        {
                            CommandId = 4,
                            Name = "stop"
                        };
                    case "cmd":
                        return new Command
                        {
                            CommandId = 5,
                            Name = "cmd"
                        };
                    case "show":
                        return new Command
                        {
                            CommandId = 6,
                            Name = "show"
                        };
                    default:
                        return new Command
                        {
                            CommandId = 7,
                            Name = "unknown"
                        };
                }
            });
        }

        private static List<Product> CreateProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Bread",
                    Price = 0.78
                },
                new Product
                {
                    Id = 2,
                    Name = "Bottle of milk",
                    Price = 1.12
                },
                new Product
                {
                    Id = 3,
                    Name = "Cheese",
                    Price = 2.17
                },
                new Product
                {
                    Id = 4,
                    Name = "Tomato",
                    Price = 0.54
                },
                new Product
                {
                    Id = 5,
                    Name = "Chocolate bar",
                    Price = 3.05
                },
                new Product
                {
                    Id = 6,
                    Name = "Bubble gum",
                    Price = 0.65
                }
            };
        }

        private static async Task AddItemToShoppingCartAsync(int productId)
        {
            await Task.Run(() => {
                Thread.Sleep(500);

                var product = products.FirstOrDefault(p => p.Id == productId);

                if(product == null)
                {
                    Console.WriteLine("Product doesn't exist. Please check the id of the product.");
                }
                else
                {
                    var cartItem = shoppingCart.Items.FirstOrDefault(i => i.Product.Id == productId);

                    if (cartItem == null)
                    {
                        lock(lockObject)
                        {
                            shoppingCart.Items.Add(new ShoppingCartItem
                            {
                                Product = products.First(p => p.Id == productId),
                                Quantity = 1
                            });
                        }
                    }
                    else
                    {
                        lock(lockObject)
                        {
                            cartItem.Quantity++;
                        }
                    }
                }
            });
        }

        private static async Task<double> CalculateTotalAmountAsync()
        {
            return await Task<double>.Run(() => {
                Thread.Sleep(1000);

                double totalAmount = 0;

                foreach(var item in shoppingCart.Items)
                {
                    totalAmount += item.Quantity * item.Product.Price;
                }

                return totalAmount;
            });
        }

        private static async Task RemoveItemFromShoppingCartAsync(int productId)
        {
            await Task.Run(() => {
                Thread.Sleep(500);

                var cartItem = shoppingCart.Items.FirstOrDefault(i => i.Product.Id == productId);

                if (cartItem != null)
                {
                    if(cartItem.Quantity != 1)
                    {
                        lock(lockObject)
                        {
                            cartItem.Quantity--;
                        }
                    }
                    else
                    {
                        lock(lockObject)
                        {
                            shoppingCart.Items.Remove(cartItem);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("This product wasn't found in your shopping cart.");
                }
            });
        }

        private static void PrintListOfProductsToConsole()
        {
            var sb = new StringBuilder();

            foreach(var product in products)
            {
                sb.AppendLine($"{product.Id}. {product.Name} - ${product.Price}");
            }

            Console.WriteLine(sb.ToString());
        }

        private static async Task ClearShoppingCartAsync()
        {
            await Task.Run(() => {
                Thread.Sleep(500);
                shoppingCart.Items.Clear();
            });
        }

        private static void PrintListOfCommandsToConsole()
        {
            Console.WriteLine("\n1. add [id] - to add item to shopping cart\n2. del [id] - to delete item from shopping cart\n3. clear - to clear shopping cart\n4. cmd - to show the list of available commands\n5. show - to show list of products\n6. stop - to finish the program\n");
        }
    }
}
