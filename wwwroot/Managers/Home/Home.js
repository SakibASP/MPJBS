
document.addEventListener("DOMContentLoaded", function (e) {
    GetIncomeExpense().then(data => {
        //alert(data.Income);
        let incomeParagaraph = document.getElementById("idTotalCollection");
        let expenseParagaraph = document.getElementById("idTotalExpense");
        let balanceParagaraph = document.getElementById("idBalance");
        //calculating balance
        let balance = parseFloat(data.Income) - parseFloat(data.Expense);
        //populating the paragraphs
        incomeParagaraph.innerText = data.Income;
        expenseParagaraph.innerText = data.Expense;
        balanceParagaraph.innerText = balance;
    })
});

const GetIncomeExpense = async function () {
    try {
        const url = "/Home/GetIncomeExpense";
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });
        if (!response.ok) {
            throw new Error("Network response was not ok!");
        }
        let data = await response.json();
        return data;
    }
    catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}