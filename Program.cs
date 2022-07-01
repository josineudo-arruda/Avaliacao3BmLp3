using Avaliacao3Bimlp3.Database;
using Avaliacao3Bimlp3.Repositories;
using Avaliacao3Bimlp3.Models;

//Routing
var modelName = args[0];
var modelAction = args[1];

var databaseConfig = new DatabaseConfig(); 
new DatabaseSetup(databaseConfig);

if(modelName == "Product")
{
    var productRepository = new ProductRepository(databaseConfig);
    if(modelAction == "List")
    {
        if(productRepository.existsAnyProduct())
        {
            Console.WriteLine("Lista de Produtos:");
            foreach (var product in productRepository.GetAll())
            {
                Console.WriteLine("{0},{1},{2},{3}", product.Id, product.Name, product.Price, product.Active);
            } 
        }
        else 
        {
            Console.WriteLine("Nenhum produto cadastrado");
        }
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var name = args[3];
        var price = Convert.ToDouble(args[4]);
        var active = Convert.ToBoolean(args[5]);

        if(productRepository.existsById(id))
        {
            Console.WriteLine($"Produto {id} já existe!");
        }
        else 
        {
            var product = new Product(id, name, price, active);
            var result = productRepository.Save(product);
            Console.WriteLine("{0}, {1}, {2}, {3}", result.Id, result.Name, result.Price, result.Active);
        }
    }

    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        
        if(productRepository.existsById(id))
        {
            productRepository.Delete(id);
            Console.WriteLine($"Produto {id} removido com sucesso!");
        }
        else 
        {
            Console.WriteLine($"Produto {id} não encontrado!");
        }
    }

    if(modelAction == "Enable")
    {
        var id = Convert.ToInt32(args[2]);
        
        if(productRepository.existsById(id))
        {
            productRepository.Enable(id);
            Console.WriteLine($"Produto {id} habilitado com sucesso!");
        }
        else 
        {
            Console.WriteLine($"Produto {id} não encontrado!");
        }
    }

    if(modelAction == "Disable")
    {
        var id = Convert.ToInt32(args[2]);
        
        if(productRepository.existsById(id))
        {
            productRepository.Disable(id);
            Console.WriteLine($"Produto {id} desabilitado com sucesso!");
        }
        else 
        {
            Console.WriteLine($"Produto {id} não encontrado!");
        }
    }

    if(modelAction == "PriceBetween")
    {
        var initialPrice = Convert.ToDouble(args[2]);
        var endPrice = Convert.ToDouble(args[3]);
        
        if(productRepository.GetAllWithPriceBetween(initialPrice,endPrice).Any())
        {
            foreach (var product in productRepository.GetAllWithPriceBetween(initialPrice,endPrice))
            {
                Console.WriteLine("{0},{1},{2},{3}", product.Id, product.Name, product.Price, product.Active);
            } 
        }
        else 
        {
            Console.WriteLine("Nenhum produto encontrada dentro do intervalo de preço {0} e {1}",initialPrice, endPrice);
        }
    }

    if(modelAction == "PriceHigherThan")
    {
        var price = Convert.ToDouble(args[2]);
        
        if(productRepository.GetAllWithPriceHigherThan(price).Any())
        {
            foreach (var product in productRepository.GetAllWithPriceHigherThan(price))
            {
                Console.WriteLine("{0},{1},{2},{3}", product.Id, product.Name, product.Price, product.Active);
            } 
        }
        else 
        {
            Console.WriteLine("Nenhum produto encontrado com preço maior que {0}",price);
        }
    }

    if(modelAction == "PriceLowerThan")
    {
        var price = Convert.ToDouble(args[2]);
        
        if(productRepository.GetAllWithPriceLowerThan(price).Any())
        {
            foreach (var product in productRepository.GetAllWithPriceLowerThan(price))
            {
                Console.WriteLine("{0},{1},{2},{3}", product.Id, product.Name, product.Price, product.Active);
            } 
        }
        else 
        {
            Console.WriteLine("Nenhum produto encontrado com preço menor que {0}", price);
        }
    }

    if(modelAction == "AveragePrice")
    {
        if(productRepository.existsAnyProduct())
        {
            var media = productRepository.GetAveragePrice();
            Console.WriteLine("A média dos preços é {0}", media);
        }
        else 
        {
            Console.WriteLine("Nenhum produto cadastrado");
        }
    }
}