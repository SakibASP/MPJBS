let members = document.getElementById("MemberId");
let payableAmount = document.getElementById("PayableAmount");
let paidAmount = document.getElementById("PaidAmount");

if (members != null) {
    members.addEventListener("change", function (e) {
        AmountCalculation(members.value);
    });
}

const AmountCalculation = function (memberId) {
    $.ajax({
        type: "GET",
        url: "/Home/GetMemberInfo",
        data: { memberId },
        dataType: "json",
        contentType: 'application/json',
        success: function (data) {
            //alert(data.Name);
            payableAmount.value = data.Amount;
        },
        error: function () {
            alert("Error occured!!")
        }
    });
}

