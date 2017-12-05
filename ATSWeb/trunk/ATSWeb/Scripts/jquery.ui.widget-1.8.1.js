/*!
* jQuery UI Widget 1.8.1
*
* Copyright (c) 2010 AUTHORS.txt (http://jqueryui.com/about)
* Dual licensed under the MIT (MIT-LICENSE.txt)
* and GPL (GPL-LICENSE.txt) licenses.
*
* http://docs.jquery.com/UI/Widget
*/
(function ($) {

    var _remove = $.fn.remove;

    $.fn.remove = function (selector, keepData) {
        return this.each(function () {
            if (!keepData) {
                if (!selector || $.filter(selector, [this]).length) {
                    $("*", this).add(this).each(function () {
                        $(this).triggerHandler("remove");
                    });
                }
            }
            //domԪ���ڱ�ɾ��ǰ������һ��remove�¼���jquery��ܱ���û�ж�Ԫ��ɾ�����¼�
            return _remove.call($(this), selector, keepData);
        });
    };

    $.widget = function (name, base, prototype) {
        var namespace = name.split(".")[0],
        fullName;
        name = name.split(".")[1];
        fullName = namespace + "-" + name;
        //����ui.tab,�����name='tab';fullName='ui-tab';

        if (!prototype) {
            prototype = base;
            base = $.Widget;
        }
        //���û��prototype,��ôprototype����base����,ʵ��baseĬ��Ϊ$.Widget

        // create selector for plugin
        $.expr[":"][fullName] = function (elem) {
            return !!$.data(elem, name);
        };

        $[namespace] = $[namespace] || {}; //�Ƿ��������ռ�
        $[namespace][name] = function (options, element) {//������������ӣ�����ʼ����$.ui.tab=func
            // allow instantiation without initializing for simple inheritance
            if (arguments.length) {
                this._createWidget(options, element);
            }
        };

        var basePrototype = new base(); //��ʼ����һ�㶼�ǵ�����new $.Widget()
        // we need to make the options hash a property directly on the new instance
        // otherwise we'll modify the options hash on the prototype that we're
        // inheriting from
        //    $.each( basePrototype, function( key, val ) {
        //        if ( $.isPlainObject(val) ) {
        //            basePrototype[ key ] = $.extend( {}, val );
        //        }
        //    });
        basePrototype.options = $.extend({}, basePrototype.options); //��ʼ��optionsֵ��ע�ⲻ��Ҫ��ȿ���
        $[namespace][name].prototype = $.extend(true, basePrototype, {
            namespace: namespace,
            widgetName: name,
            widgetEventPrefix: $[namespace][name].prototype.widgetEventPrefix || name,
            widgetBaseClass: fullName
        }, prototype);
        //Ϊ�µ�uiģ�鴴��ԭ�ͣ�ʹ����ȿ�������basePrototype����չһЩģ�������Ϣ������չprototype,����ui.tabs.js�о���tab��ӵ�и��ַ����Ĵ����

        $.widget.bridge(name, $[namespace][name]); //���˷�������jQuery������
    };

    $.widget.bridge = function (name, object) {
        $.fn[name] = function (options) {
            var isMethodCall = typeof options === "string",
            args = Array.prototype.slice.call(arguments, 1),
            returnValue = this;
            //�����һ��������string���ͣ�����Ϊ�ǵ���ģ�鷽��
            //ʣ�µĲ�����Ϊ�����Ĳ�����������õ�

            // allow multiple hashes to be passed on init
            options = !isMethodCall && args.length ?
            $.extend.apply(null, [true, options].concat(args)) :
            options;
            //���Լ���Ϊ��$.extend(true,options,args[0],...),args������һ��������������

            // prevent calls to internal methods
            if (isMethodCall && options.substring(0, 1) === "_") {
                return returnValue;
            }
            //��ͷ���»��ߵķ�������˽�з��������õ���

            if (isMethodCall) {//����ǵ��ú���
                this.each(function () {
                    var instance = $.data(this, name), //�õ�ʵ����ʵ����Ϊһ�����ݺ�Ԫ�ع�����
                    methodValue = instance && $.isFunction(instance[options]) ?
                        instance[options].apply(instance, args) : //���ʵ���ͷ��������ڣ����÷�������args��Ϊ��������ȥ
                        instance; //���򷵻�undefined
                    if (methodValue !== instance && methodValue !== undefined) {//���methodValue����jquery����Ҳ����undefined
                        returnValue = methodValue;
                        return false; //����each��һ���ȡoptions��ֵ���������֧
                    }
                });
            } else {//���Ǻ������õĻ�
                this.each(function () {
                    var instance = $.data(this, name);
                    if (instance) {//ʵ������
                        if (options) {//�в���
                            instance.option(options); //����option������һ��������״̬֮��Ĳ���
                        }
                        instance._init(); //�ٴε��ô˺���������options����
                    } else {
                        $.data(this, name, new object(options, this));
                        //û��ʵ���Ļ�����Ԫ�ذ�һ��ʵ����ע�������this��dom��object��ģ����
                    }
                });
            }

            return returnValue; //���أ��п�����jquery�����п���������ֵ
        };
    };

    $.Widget = function (options, element) {//����ģ��Ļ���
        // allow instantiation without initializing for simple inheritance
        if (arguments.length) {//����в��������ó�ʼ������
            this._createWidget(options, element);
        }
    };

    $.Widget.prototype = {
        widgetName: "widget",
        widgetEventPrefix: "",
        options: {
            disabled: false
        }, //��������Ի��ڴ���ģ��ʱ������
        _createWidget: function (options, element) {
            // $.widget.bridge stores the plugin instance, but we do it anyway
            // so that it's stored even before the _create function runs
            this.element = $(element).data(this.widgetName, this); //����ʵ��������jquery����
            this.options = $.extend(true, {},
            this.options,
            $.metadata && $.metadata.get(element)[this.widgetName],
            options); //��������

            var self = this;
            this.element.bind("remove." + this.widgetName, function () {
                self.destroy();
            }); //ע�������¼�

            this._create(); //����
            this._init(); //��ʼ��
        },
        _create: function () { },
        _init: function () { },

        destroy: function () {//����ģ�飺ȥ�����¼���ȥ�����ݡ�ȥ����ʽ������
            this.element
            .unbind("." + this.widgetName)
            .removeData(this.widgetName);
            this.widget()
            .unbind("." + this.widgetName)
            .removeAttr("aria-disabled")
            .removeClass(
                this.widgetBaseClass + "-disabled " +
                "ui-state-disabled");
        },

        widget: function () {//����jquery����
            return this.element;
        },

        option: function (key, value) {//����ѡ���
            var options = key,
            self = this;

            if (arguments.length === 0) {
                // don't return a reference to the internal hash
                return $.extend({}, self.options); //����һ���µĶ��󣬲����ڲ����ݵ�����
            }

            if (typeof key === "string") {
                if (value === undefined) {
                    return this.options[key]; //ȡֵ
                }
                options = {};
                options[key] = value; //����ֵ
            }

            $.each(options, function (key, value) {
                self._setOption(key, value); //�����ڲ���_setOption
            });

            return self;
        },
        _setOption: function (key, value) {
            this.options[key] = value;

            if (key === "disabled") {//���ӻ���ȥ��className
                this.widget()
                [value ? "addClass" : "removeClass"](
                    this.widgetBaseClass + "-disabled" + " " +
                    "ui-state-disabled")
                .attr("aria-disabled", value);
            }

            return this;
        },

        enable: function () {
            return this._setOption("disabled", false);
        },
        disable: function () {
            return this._setOption("disabled", true);
        },

        _trigger: function (type, event, data) {
            var callback = this.options[type];

            event = $.Event(event);
            event.type = (type === this.widgetEventPrefix ?
            type :
            this.widgetEventPrefix + type).toLowerCase();
            data = data || {};

            // copy original event properties over to the new event
            // this would happen if we could call $.event.fix instead of $.Event
            // but we don't have a way to force an event to be fixed multiple times
            if (event.originalEvent) {//��ԭʼ��event�������¸���event������
                for (var i = $.event.props.length, prop; i; ) {
                    prop = $.event.props[--i];
                    event[prop] = event.originalEvent[prop];
                }
            }

            this.element.trigger(event, data);

            return !($.isFunction(callback) &&
            callback.call(this.element[0], event, data) === false ||
            event.isDefaultPrevented());
        }
    };

})(jQuery);