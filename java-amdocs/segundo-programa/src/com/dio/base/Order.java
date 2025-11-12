package com.dio.base;

import java.math.BigDecimal;

/**
 * @author thiago
 * @version 1.0.0
 * @see BigDecimal
 * @since 1.0.0
 */

public class Order {

    private final String code;
    private final BigDecimal totalValue;
    private String[] items;

    /**
     *  @param code          Código do Pedido
     * @param totalValue    Valor total do Pedido
     */
    public Order(String code, BigDecimal totalValue) {
        this.code = code;
        this.totalValue = totalValue;

    }

/*    public void printItems() {
        // int i = 0;
        *//*        while (i < items.length) {
            System.out.println(items[i]);
            i++;
        }*//*

        do {

        } while (i < items.length);

        for (int i=0; i < items.length; i++) {

        }

        // enhanced for
        for (String i : items) {
            System.out.println(i);
        }
    }*/

/*    public double calculateFee() {
        if (totalValue > 100) {
            return totalValue * 0.99;
        } else {
            return totalValue;
        }
    }

    public double calculateFee2() {
        switch (totalValue) {
            case 100:
                return totalValue * 0.99;
            case 200:
                return totalValue * 1.99;
            case default:
                return totalValue;
        }
    }*/

    /**
     *
     * @return Valor total do Pedido com as taxas
     * @throws RuntimeException Se o pedido for negativo
     */
    public BigDecimal calculateFee() throws RuntimeException {

        if (this.totalValue.signum() < 0) {
            throw new RuntimeException("O pedido nao pode ter valor negativo");
        }
        if (this.totalValue.compareTo(new BigDecimal("100.00")) > 100) {
            return this.totalValue.multiply(new BigDecimal("0.99"));
        }
        return this.totalValue;
    }

    @Override
    public String toString() {
        return "Order = {" +
                "code='" + code + "'" +
                "totalValue='" + totalValue + "'" +
                "}";
    }

}

// Gerar o JavaDoc
// javadoc -d javadoc/ -sourcepath src/ -subpackages com.dio