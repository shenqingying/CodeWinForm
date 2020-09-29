$(document).ready(function () {

    $(document).on("click", ".nav-bar-node", function () {
        document.cookie = "navBar=" + escape($(this).attr("id")) + ";path=/";
    });


    var node = jQuery.parseJSON($("input#nav-bar-node").val());

    $(function () {
        new AccordionMenu({ menuArrs: node.Children });
    });

    $(".sign-out").click(function () {

        var uri = $(this).next().val();
        warningDialog('确认退出?', function () { location.href = uri });
    });
});




var id = 0;


function AccordionMenu(options) {
    this.config = {
        containerCls: '.nav-bar-menu',    // 外层容器
        menuArrs: '',                     // JSON传进来的数据
        type: 'click',                    // 默认为click 也可以mouseover
        renderCallBack: null,             // 渲染html结构后回调
        clickItemCallBack: null           // 每点击某一项时候回调
    };
    this.cache = {

    };
    this.init(options);
}


AccordionMenu.prototype = {

    constructor: AccordionMenu,

    init: function (options) {
        this.config = $.extend(this.config, options || {});
        var self = this, _config = self.config, _cache = self.cache;

        // 渲染html结构
        $(_config.containerCls).each(function (index, item) {

            self._renderHTML(item);

            // 处理点击事件
            self._bindEnv(item);
        });
    },
    _renderHTML: function (container) {
        var self = this, _config = self.config, _cache = self.cache;
        var ulhtml = $('<ul></ul>');
        $(_config.menuArrs).each(function (index, item) {
            var lihtml = $('<li><h2>' + item.Node + '</h2></li>');

            if (item.Children && item.Children.length > 0) {
                self._createSubMenu(item.Children, lihtml);
            }
            $(ulhtml).append(lihtml);
        });
        $(container).append(ulhtml);

        _config.renderCallBack && $.isFunction(_config.renderCallBack) && _config.renderCallBack();

        // 处理层级缩进
        self._levelIndent(ulhtml);
    },
    /**
    * 创建子菜单
    * @param {array} 子菜单
    * @param {lihtml} li项
    */
    _createSubMenu: function (Children, lihtml) {
        var self = this,
            _config = self.config,
            _cache = self.cache;
        var subUl = $('<ul></ul>'),
            callee = arguments.callee,
            subLi;

        $(Children).each(function (index, item) {
            var uri = item.Uri || 'javascript:void(0)';

            subLi = $('<li><a class="nav-bar-node" id="' + id + '" href="' + uri + '" target="_blank">' + item.Node + '</a></li>');
            id++;

            if (item.Children && item.Children.length > 0) {

                $(subLi).children('a').prepend('<img src="' + $("input#blankImageUrl").val() + '" alt=""/>');
                callee(item.Children, subLi);
            }
            $(subUl).append(subLi);
        });
        $(lihtml).append(subUl);
    },
    /**
    * 处理层级缩进
    */
    _levelIndent: function (ulList) {
        var self = this,
            _config = self.config,
            _cache = self.cache,
            callee = arguments.callee;

        var initTextIndent = 2,
            lev = 1,
            $oUl = $(ulList);

        while ($oUl.find('ul').length > 0) {
            initTextIndent = parseInt(initTextIndent, 10) + 1 + 'em';
            $oUl.children().children('ul').addClass('lev-' + lev).children('li').css('text-indent', initTextIndent);
            $oUl = $oUl.children().children('ul');
            lev++;
        }
        $(ulList).find('ul').hide();
        //$(ulList).find('ul:first').show();
        self._LoadStatus();
    },
    /**
    * 绑定事件
    */
    _bindEnv: function (container) {
        var self = this,
            _config = self.config;

        $('h2,a', container).unbind(_config.type);
        $('h2,a', container).bind(_config.type, function (e) {
            if ($(this).siblings('ul').length > 0) {
                $(this).siblings('ul').slideToggle('fast').end().children('img').toggleClass('unfold');
            }

            $(this).parent('li').siblings().find('ul').slideUp('fast').end().find('img.unfold').removeClass('unfold');
            _config.clickItemCallBack && $.isFunction(_config.clickItemCallBack) && _config.clickItemCallBack($(this));

        });
    },
    /**
    * 获取cookie
    */
    _GetCookie: function () {
        var key = "navBar";
        var cookie = document.cookie;
        var start = cookie.indexOf(key + "=");
        var len = start + key.length + 1;
        if ((!start) && (key != cookie.substring(0, key.length))) {
            return 0;
        }
        if (start == -1)
            return 0;
        var end = cookie.indexOf(';', len);
        if (end == -1)
            end = cookie.length;
        return unescape(cookie.substring(len, end));
    },
    /**
    * 加载状态
    */
    _LoadStatus: function () {
        var sn = this._GetCookie();
        if (sn < 0)
            sn = 0;

        var node = $("a#" + sn);
        do {
            node.show();
            node = node.parent().parent();
        } while (node.attr("class") != "nav-bar color-nav-bar")
    }

};