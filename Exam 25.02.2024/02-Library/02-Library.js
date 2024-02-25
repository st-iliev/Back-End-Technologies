function library(input){
    const numberOfBooks = parseInt(input.shift());
    const books = input.slice(0,numberOfBooks);
    for (let index = 0; index < input.length; index++) {
        const command = input[index].split(' ');
        if(command[0] === 'Lend'){
           const removedBook = books.shift();
            console.log(`${removedBook} book lent!`)
        }else if (command[0] === 'Return'){
            const newBook = input[index].slice(7);
            books.unshift(newBook);
        }else if(command[0] === `Exchange`){
            const startIndex = parseInt(command[1]);
            const endIndex = parseInt(command[2]);
            const bookAtStartIndex = books[startIndex];
            books[startIndex] = books[endIndex];
            books[endIndex] = bookAtStartIndex;
            console.log('Exchanged!');
        }else if (command[0] === 'Stop'){
          break;
        }  
    }
    if(books.length>0){
        console.log(`Books left: ${books.join(', ')}`);
       }else{
        console.log("The library is empty");
       }
}
//library(['3', 'Harry Potter', 'The Lord of the Rings', 'The Hunger Games', 'Lend', 'Stop', 'Exchange 0 1']);
//library(['5', 'The Catcher in the Rye', 'To Kill a Mockingbird', 'The Great Gatsby', '1984', 'Animal Farm', 'Return Brave New World', 'Exchange 1 4', 'Stop']);
//library(['3', 'The Da Vinci Code', 'The Girl with the Dragon Tattoo', 'The Kite Runner', 'Lend', 'Lend', 'Lend', 'Stop']);