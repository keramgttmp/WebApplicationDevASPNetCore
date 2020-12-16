document.addEventListener('DOMContentLoaded', () => {
    console.log('Document ready!');

    const filter = document.getElementById('filter');
    const bSearch = document.getElementById('bSearch');
    bSearch.addEventListener('click', (event) => {
        event.preventDefault();
        document.getElementById('list').innerHTML = ``;
        getProducts(filter.value);
    });
});

function appendProduct(item) {
    document.getElementById('list').innerHTML +=`
        <li class="list-group-item">
        <a href="/Product/Details/${item.productId}">${item.productName}</a>
        <p>Nombre ${item.productName}</p>
        <p>Precio Unit. ${item.unitPrice}</p>
        <p>Categoría {item.category.categoryName}</p>
    </li>`;
}

function appendCategory(categoryName) {
    document.getElementById('list').innerHTML += `<tr><td colspan='6'><h4>${categoryName}</h4></td></tr>`;
}

function getProducts(pfilter) {
    axios
        .get('https://localhost:44347/home/json?pfilter=' + pfilter)
        .then(function (response) {
            console.log(response);
            for (let vItem of response.data){
                appendProduct(vItem);
            }
        })
        .catch(function (error) {
            console.log(error);
        })
        .then(function () { });
}

class ProductService {
    getProducts() {
        //return fetch('js/index.json').then(
        //    (response) => {
        //        if (response.status !== 200) {
        //            console.log('http not 200');
        //        }

        //        return response.json();
        //    });
        $.ajax({
            type: "GET",
            url: 'https://localhost:44347/home/json',
            //data: { number1: val1, number2: val2 },
            dataType: JSON,
            success: function (data) {
                return data;
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
}