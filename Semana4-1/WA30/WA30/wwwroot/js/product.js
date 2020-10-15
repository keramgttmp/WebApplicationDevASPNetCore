document.addEventListener('DOMContentLoaded', () => {
    console.log('Document ready!');

    const bSearch = document.getElementById('bSearch');
    const filter = document.getElementById('filter');

    bSearch.addEventListener('click', (event) => {
        event.preventDefault();

        let ps = new ProductService();

        ps.getProducts().then((data) => {
            console.log(data);
            //filtro con underscore
            let filtered = _.filter(data, function (p) {
                return p.productName.toLowerCase().indexOf(filter.value.toLowerCase()) !== -1;
            });

            console.log(filtered);

        }).catch((err) => {
            console.log(`Error: ${err}`);
        });

    });

});

function appendProduct(item) {
    document.getElementById('list').innerHTML += `<td>${item.productName}</td>
            <td>${item.quantityPerUnit}</td>
            <td>${item.unitPrice}</td>
            <td>${item.unitsInStock}</td>
            <td>${item.category.categoryName}</td>
            <td>${item.supplier.companyName}</td>`;
}

function appendCategory(categoryName) {
    document.getElementById('list').innerHTML += `<tr><td colspan='6'><h4>${categoryName}</h4></td></tr>`;
}

class ProductService {
    getProducts() {
        return fetch('js/index.json').then(
            (response) => {
                if (response.status !== 200) {
                    console.log('http not 200');
                }

                return response.json();
            });
    }
}