public static class ProductRepository{
  public static List<Product> Products { get; set; } = Products = new List<Product>();
  
  public static void Init(IConfiguration configuration){
    var initialProducts = configuration.GetSection("Products").Get<List<Product>>();
    Products = initialProducts;
  }
  public static void Add(Product product){
    Products.Add(product);
  }

  public static Product Find(string code){
    return Products.FirstOrDefault(x => x.Code == code);
  }

  public static void Delete(Product product){
    Products.Remove(product);
  }
}
