function cinema ( input){
    const numberOfMovies = parseInt(input[0]);
    const movies = input.slice(1,numberOfMovies+1);
    const commands = input.slice(numberOfMovies+1);

    for (let index = 0; index < commands.length-1; index++) {
        const allCommands = commands[index].split(' ');
        const command = allCommands[0];
        if(command === 'Sell'){
           const firstMovie= movies.shift();
           console.log(`${firstMovie} ticket sold!`);
        }else if (command === 'Add'){
            const movieTitle = commands[index].slice(4);
            movies.push(movieTitle);
        }else if (command === 'Swap'){
            const startIndex = parseInt(allCommands[1]);
            const endIndex = parseInt(allCommands[2]);
            if(startIndex < 0 || startIndex >= movies.length){
                continue;
            }
            if(endIndex < 0 || endIndex >= movies.length){
                continue;
            }
            const movieAtFirstIndex = movies[startIndex];
            movies[startIndex] = movies[endIndex];
            movies[endIndex] = movieAtFirstIndex;
            console.log('Swapped!');
        }else if (command === 'End'){
            break;
        }
    }
    if(movies.length > 0){
        console.log(`Tickets left: ${movies.join(', ')}`)
    }else{
        console.log('The box office is empty');
    }
}

//cinema(['3','Avatar', 'Titanic', 'Joker', 'Sell', 'Swap 0 1', 'End'])

cinema(['5', 'The Matrix', 'The Godfather', 'The Shawshank Redemption', 'The Dark Knight', 'Inception', 'Add The Lord of the Rings', 'Swap 1 4', 'End']);

//cinema(['3', 'Star Wars', 'Harry Potter', 'The Hunger Games','Sell', 'Sell', 'Sell', 'End']);