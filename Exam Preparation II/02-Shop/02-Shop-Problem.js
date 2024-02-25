function shop(input){
    const numberOfProducts = parseInt(input.shift());
    const products = input.slice(0,numberOfProducts);
    for (let index = 0; index < input.length; index++) {
        const command = input[index].split(' ');
        if(command[0]=== 'Sell'){
            const removedProduct = products.shift();
            console.log(`${removedProduct} product sold!`);
        }else if(command[0] === 'Add'){
            products.push(command[1]);
        }else if(command[0] === 'Swap'){
            const startIndex = parseInt(command[1]);
            const endIndex = parseInt(command[2]);
            const productAtStartIndex = products[startIndex];
            products[startIndex] = products[endIndex];
            products[endIndex] = productAtStartIndex;
            console.log("Swapped!");
        }else if(command[0] === 'End'){
            break; 
        }
        
    }
    if(products.length>0){
        console.log(`Products left: ${products.join(', ')}`);
    }else{
        console.log("The shop is empty");
    }
}
//shop(['3', 'Apple', 'Banana', 'Orange', 'Sell', 'End', 'Swap 0 1']);
//shop(['5', 'Milk', 'Eggs', 'Bread', 'Cheese', 'Butter', 'Add Yogurt', 'Swap 1 4', 'End']);
//shop(['3', 'Shampoo', 'Soap', 'Toothpaste', 'Sell', 'Sell', 'Sell', 'End']);