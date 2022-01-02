pm.test("Status code is 200 and body is not null", function () {
    pm.response.to.have.status(200);
    pm.response.to.have.body;
});
pm.test("Response pagination is correct", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.pageSize).to.eql(10);
    pm.expect(jsonData.currentPageNumber).to.eql(1);
    pm.expect(jsonData.lastPageNumber).to.eql(10);
});
pm.test("Body contains exectly 10 items in response", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.items.length).to.eql(10);
});
pm.test("Every document in response has included resource", function () {
    var jsonData = pm.response.json();
    jsonData.items.forEach((result) => {
        pm.expect(result.includes).to.have.property('clients');
    });
});