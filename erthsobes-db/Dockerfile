#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM postgres:12.4 AS base
COPY init.sql /docker-entrypoint-initdb.d/
