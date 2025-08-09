import Fastify from 'fastify';
import dotenv from 'dotenv';
import helmet from '@fastify/helmet';
import cors from '@fastify/cors';
import Routes from './routes/routes.one.js';

dotenv.config();

const PORT = process.env.PORT
const HOST = process.env.HOST



const fastify = Fastify({ logger: false });



await fastify.register(helmet);
await fastify.register(cors, {
    origin: process.env.CORS_ORIGIN || '*',
    credentials: true,
});

fastify.register(Routes, { prefix: '/' });

fastify.setErrorHandler((error, request, reply) => {
    request.log.error(error);
    reply.status(500).send({ error: 'Internal Server Error' });
});



const start = async () => {
    try {
        await fastify.listen({ port: PORT, host: HOST }); // use 0.0.0.0 for Docker/VM
        console.log(`AuthServer running at http://${HOST}:${PORT}`);
    } catch (err) {
        fastify.log.error(err);
        process.exit(1);
    }
};

start();

process.on('SIGINT', () => fastify.close());
process.on('SIGTERM', () => fastify.close());
