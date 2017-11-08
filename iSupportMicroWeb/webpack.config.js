var path = require('path')
var webpack = require('webpack')
const UglifyJSPlugin = require('uglifyjs-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = (env) => {
    const isDevBuild = !(env && env.production);

    return [{
        entry: { site: './wwwroot/js/main.js' },
        output: {
            filename: 'bundle.js',
            path: path.resolve(__dirname, 'wwwroot/dist/')
        },
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    loader: 'ts-loader',
                    exclude: /node_modules/,
                },
                {
                    test: /\.vue$/,
                    loader: 'vue-loader',
                    options: {
                        loaders: {
                            scss: ExtractTextPlugin.extract({
                                use: 'css-loader!sass-loader',
                                fallback: 'vue-style-loader'
                            }),
                            sass: ExtractTextPlugin.extract({
                                use: 'css-loader!sass-loader?indentedSyntax',
                                fallback: 'vue-style-loader'
                            })
                        }
                        // other vue-loader options go here
                    }
                },
                {
                    test: /\.css$/, use: ['style-loader', 'css-loader']
                },
                {
                    test: /\.jsx$/,
                    loader: 'babel'
                },
                {
                    test: /\.js$/,
                    loader: 'babel-loader',
                    exclude: /node_modules/
                },
                {
                    test: /\.(png|jpg|gif|svg)$/,
                    loader: 'file-loader',
                    options: {
                        name: '[name].[ext]?[hash]'
                    }
                },
                { test: /\.(woff|woff2|eot|ttf)$/, loader: 'url-loader?limit=100000' }
            ]
        },
        plugins: [
            new webpack.optimize.UglifyJsPlugin({
                sourceMap: true,
                compress: {
                    warnings: false
                }
            }),
            new ExtractTextPlugin({
                filename: 'site.css'
            }),
            //new webpack.ProvidePlugin({
            //    $: 'jquery',
            //    jquery: 'jquery',
            //    'window.jQuery': 'jquery',
            //    jQuery: 'jquery'
            //})
        ],
        resolve: {
            alias: {
                'vue$': 'vue/dist/vue.esm.js'
            },
            extensions: [".tsx", ".ts", ".js"]
        }
    }];
};