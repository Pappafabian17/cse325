# CSE 325 - Assignment Notes

## Item 1: Web API Evidence (CRUD Operations & Additional Record)

### Initial State with Additional Record
The base list originally contained 2 pizzas (`Classic Italian` and `Veggie`). An additional record (`Pepperoni`, Id: 3) was added to `PizzaService.cs`.

---

### 1. GET Operation
* **Request:**
  ```http
  GET /pizza
  Accept: application/json
  ```
* **Response Status Code:** `200 OK`
* **Response Body:**
  ```json
  [
    {
      "id": 1,
      "name": "Classic Italian",
      "isGlutenFree": false
    },
    {
      "id": 2,
      "name": "Veggie",
      "isGlutenFree": true
    },
    {
      "id": 3,
      "name": "Pepperoni",
      "isGlutenFree": false
    }
  ]
  ```

---

### 2. POST Operation (Create)
* **Request:**
  ```http
  POST /pizza
  Content-Type: application/json

  {
    "name": "Hawaiian",
    "isGlutenFree": false
  }
  ```
* **Response Status Code:** `201 Created`
* **Response Body:**
  ```json
  {
    "id": 4,
    "name": "Hawaiian",
    "isGlutenFree": false
  }
  ```

---

### 3. PUT Operation (Update)
* **Request:**
  ```http
  PUT /pizza/3
  Content-Type: application/json

  {
    "id": 3,
    "name": "Pepperoni Supreme",
    "isGlutenFree": true
  }
  ```
* **Response Status Code:** `204 No Content`

---

### 4. DELETE Operation
* **Request:**
  ```http
  DELETE /pizza/2
  ```
* **Response Status Code:** `204 No Content`

---

## Item 2: Sales Summary Function (Working Copy)

Below is the text copy of the working C# function implemented in `Program.cs` for the *Work with files and directories in a .NET app* module, which generates the sales summary report file:

```csharp
void GenerateSalesSummary(IEnumerable<string> files, string outputFile)
{
    var report = new StringBuilder();
    double totalSales = 0;
    var details = new StringBuilder();

    foreach (var file in files)
    {
        string salesJson = File.ReadAllText(file);
        
        SalesTotal? data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
        
        double fileSales = data?.Total ?? 0;
        totalSales += fileSales;

        string fileName = Path.GetFileName(file);
        string parentDir = Path.GetFileName(Path.GetDirectoryName(file) ?? "");
        string displayName = string.IsNullOrEmpty(parentDir) ? fileName : $"{parentDir}/{fileName}";

        details.AppendLine($" {displayName}: {fileSales:C}");
    }

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {totalSales:C}");
    report.AppendLine();
    report.AppendLine("Details:");
    report.Append(details.ToString());

    File.WriteAllText(outputFile, report.ToString());
}
```