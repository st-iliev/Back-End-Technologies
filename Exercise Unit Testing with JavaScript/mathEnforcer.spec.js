import { mathEnforcer } from './mathEnforcer.js'
import { expect } from 'chai'

describe("mathEnforcer", ()=>{
    describe("addFive", ()=>{
        it("should return undefined if given a string as input",()=> {
            //Arrange
            const input = "string";
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.undefined;
        })
        it("should return correct result if given a floating number as input",()=> {
            //Arrange
            const input = 0.01;
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.equal(5.01);
        })

        it("should return correct result if given a floating number as input",()=> {
            //Arrange
            const input = 0.0000000001;
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.closeTo(5.01, 0.01);
        })
        it("should return correct result if given a  number as input",()=> {
            //Arrange
            const input = 5;
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.equal(10);
        })

        it("should return correct result if given a negative number as input",()=> {
            //Arrange
            const input = -77;
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.equal(-72);
        })

        it("should return zero if given a negative number as input",()=> {
            //Arrange
            const input = -5;
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.equal(0);
        })
        it("should return undefined if given a number as string ",()=> {
            //Arrange
            const input = '10';
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.undefined;
        })
        it("should return undefined if given a undefined as input",()=> {
            //Arrange
            const input = undefined;
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.undefined;
        })
        it("should return correct result if given a  floating number as input ",()=> {
            //Arrange
            const input = 9.01;
            //Act
            const result = mathEnforcer.addFive(input);
            //Assert
            expect(result).to.be.closeTo(14.01, 0.01);
        })
    })
    describe("subtractTen", ()=>{
        it("should return correct result if given a floating number as input with a closeTo assertion",()=> {
            //Arrange
            const input = 10.01;
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.closeTo(0.01, 0.01);
        })
        it("should return correct result if given a floating number as input",()=> {
            //Arrange
            const input = 15.01;
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.equal(5.01);
        })
        it("should return correct result if given a floating number",()=> {
            //Arrange
            const input = 0.0000000001;
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.closeTo(-9.99, 0.01);
        })
        it("should return undefined if given a string as input",()=> {
            //Arrange
            const input = "string";
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.undefined;
        })
        it("should return undefined if given a undefined as input",()=> {
            //Arrange
            const input = undefined;
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.undefined;
        })
        it("should return undefined if given a string number as input",()=> {
            //Arrange
            const input = "5";
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.undefined;
        })
        it("should return correct result if given a negative number as input",()=> {
            //Arrange
            const input = -15;
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.equal(-25);
        })
        it("should return correct result if given a positive number as input",()=> {
            //Arrange
            const input = 10;
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.equal(0);
        })
        it("should return correct result if given a  number as input",()=> {
            //Arrange
            const input = 4;
            //Act
            const result = mathEnforcer.subtractTen(input);
            //Assert
            expect(result).to.be.equal(-6);
        })
    })
    describe("sum", ()=>{
        it("should return undefined if given incorrect firstParameter and correct secondParameter", ()=>
        {
            //Arrange
            const firstParameter = "string";
            const secondParameter = 4;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.undefined;
        })

        it("should return undefined if given correct firstParameter and incorrect secondParamter", ()=>
        {
            //Arrange
            const firstParameter = 8;
            const secondParameter = "string";
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.undefined;
        })

        it("should return undefined if given  incorrect firstParameter and incorrect secondParameter", ()=>
        {
            //Arrange
            const firstParameter = "test";
            const secondParameter = "passed";
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.undefined;
        })

        it("should return undefined if given a firstParameter as number as string and correct secondParamter", ()=>
        {
            //Arrange
            const firstParameter = '10';
            const secondParameter = 5;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.undefined;
        })

        it("should return undefined if given a correct firstParameter and secondParameter as number as string", ()=>
        {
            //Arrange
            const firstParameter = 5;
            const secondParameter = '90';
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.undefined;
        })

        it("should return undefined if given a both parameters correct", ()=>
        {
            //Arrange
            const firstParameter = 95;
            const secondParameter = 15;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.equal(110);
        })

        it("should return undefined if given a both parameters as negative numbers", ()=>
        {
            //Arrange
            const firstParameter = -6;
            const secondParameter = -24;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.equal(-30);
        })

        it("should return undefined if given firstParameter negative number and secondParameter as positive number", ()=>
        {
            //Arrange
            const firstParameter = -97;
            const secondParameter = 97;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.equal(0);
        })

        it("should return undefined if given a firstParameter as floating number and secondParameter as positive number", ()=>
        {
            //Arrange
            const firstParameter = 6.01;
            const secondParameter = 3;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.equal(9.01);
        })

        it("should return undefined if given a firstParameteras  floating number and secondParameter as negative number with closeTo assertion", ()=>
        {
            //Arrange
            const firstParameter = 0.000000001;
            const secondParameter = 5;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.closeTo(5.01, 0.01);
        })

        it("should return undefined if given a firstParameter as floating number and secondParameter as negative number with closeTo assertion", ()=>
        {
            //Arrange
            const firstParameter = 5.01;
            const secondParameter = 5.01;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.closeTo(10.02, 0.01);
        })

        it("should return undefined if given a firstParameter as number and secondParameter as float number with closeTo assertion", ()=>
        {
            //Arrange
            const firstParameter = 0;
            const secondParameter = 0.1;
            //Act
            const result = mathEnforcer.sum(firstParameter, secondParameter);
            //Assert
            expect(result).to.be.closeTo(0.1, 0.01);
        })
    })
})