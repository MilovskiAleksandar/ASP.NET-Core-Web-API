let getUsers = document.getElementById("getUsers");
let getOneUser = document.getElementById("getOneUser");
let formInput = document.getElementById("input1");

let port = "5213";

let getAllUsers = async() => {
    let url = "http://localhost:" + port + "/api/users";

    let response = await fetch(url);
    console.log(response);

    let data = await response.json();
    console.log(data)
}
getUsers.addEventListener("click", getAllUsers);

let getOnlyOneUser = async() => {
    let url = "http://localhost:" + port + "/api/users/" + formInput.value;
    let response = await fetch(url);
    let data = await response.text();
    console.log(data)

}
getOneUser.addEventListener("click", getOnlyOneUser);
